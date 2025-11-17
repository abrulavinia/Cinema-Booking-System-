<template>
  <div class="tickets-page">
    <section class="hero">
      <h1 class="hero-title">Tickets</h1>
      <p class="hero-sub">Buy, view and cancel your tickets easily.</p>
    </section>

    <section class="container ga-6">
      <v-row dense>

        <v-col cols="12" md="6">
          <v-card class="card" elevation="0">
            <v-card-title class="pb-0">Buy ticket</v-card-title>
            <v-card-text>
              <v-row dense>
                <v-col cols="12" md="5">
                  <v-text-field
                      v-model="buyForm.screeningId"
                      type="number"
                      label="Screening ID"
                      variant="outlined"
                      :rules="[rules.req, rules.pos]"
                  />
                </v-col>
                <v-col cols="12" md="7">
                  <v-text-field
                      v-model="buyForm.email"
                      label="Email"
                      variant="outlined"
                      :rules="[rules.req, rules.email]"
                  />
                </v-col>
                <v-col cols="12" class="d-flex ga-2">
                  <v-btn :loading="busy.buy" @click="buy">
                    <v-icon start>mdi-cart</v-icon> Buy
                  </v-btn>
                  <v-btn variant="tonal" @click="prefillFromQuery" v-if="route.query.screeningId">
                    Use query ({{ route.query.screeningId }})
                  </v-btn>
                </v-col>
              </v-row>

              <v-alert
                  v-if="lastTicket"
                  type="success"
                  variant="tonal"
                  class="mt-4"
              >
                <div class="mb-1"><strong>Purchased:</strong> #{{ lastTicket.id }}</div>
                <div v-if="lastTicket.screening?.movie?.title">
                  {{ lastTicket.screening.movie.title }} — {{ fmt(lastTicket.purchasedAt) }}
                </div>
              </v-alert>
            </v-card-text>
          </v-card>
        </v-col>

        <v-col cols="12" md="6">
          <v-card class="card" elevation="0">
            <v-card-title class="pb-0">My tickets</v-card-title>
            <v-card-text>
              <v-row dense>
                <v-col cols="12" md="8">
                  <v-text-field
                      v-model="mine.email"
                      label="Email"
                      variant="outlined"
                      :rules="[rules.req, rules.email]"
                  />
                </v-col>
                <v-col cols="12" md="4" class="d-flex align-end">
                  <v-btn block :loading="busy.mine" @click="loadMine">
                    <v-icon start>mdi-magnify</v-icon> Load
                  </v-btn>
                </v-col>
              </v-row>

              <v-data-table
                  class="elevation-0 mt-3"
                  density="comfortable"
                  :items="mine.rows"
                  :headers="mine.headers"
                  :items-per-page="5"
                  item-key="id"
              >
                <template #item.movie="{ item }">
                  {{ item.screening?.movie?.title || '—' }}
                </template>
                <template #item.time="{ item }">
                  {{ fmt(item.purchasedAt) }}
                </template>
                <template #item.actions="{ item }">
                  <v-btn
                      size="small"
                      variant="text"
                      color="red"
                      @click="cancel(item)"
                  >
                    <v-icon start>mdi-close</v-icon> Cancel
                  </v-btn>
                </template>

                <template #no-data>
                  <v-alert type="info" variant="tonal" class="ma-4">
                    No tickets for this email yet.
                  </v-alert>
                </template>
              </v-data-table>
            </v-card-text>
          </v-card>
        </v-col>
      </v-row>
    </section>

    <v-snackbar v-model="snackbar.show" :color="snackbar.color" timeout="2200">
      {{ snackbar.text }}
    </v-snackbar>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { api } from '@/services/api'

const route = useRoute()

const rules = {
  req : v => !!String(v || '').trim() || 'Required',
  email: v => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(String(v||'')) || 'Invalid email',
  pos : v => Number(v)>0 || 'Must be > 0',
}

const busy = ref({ buy:false, mine:false })
const snackbar = ref({ show:false, text:'', color:'success' })
const notify = (t, type='success') => snackbar.value = { show:true, text:t, color: type==='error' ? 'red' : 'green' }

const fmt = d => new Date(d).toLocaleString()

const buyForm = ref({
  screeningId: route.query.screeningId ? Number(route.query.screeningId) : '',
  email: ''
})
const lastTicket = ref(null)

async function buy(){
  const id = Number(buyForm.value.screeningId)
  const em = (buyForm.value.email || '').trim()
  if(!(rules.pos(id)===true && rules.email(em)===true && rules.req(em)===true)) return

  busy.value.buy = true
  try{
    const { data } = await api.post(`/tickets/buy/${id}`, null, { params: { email: em } })
    lastTicket.value = data
    notify('Ticket purchased')
  }catch(e){
    notify(e?.response?.data?.message || 'Purchase failed', 'error')
  }finally{
    busy.value.buy = false
  }
}

function prefillFromQuery(){
  if(route.query.screeningId) buyForm.value.screeningId = Number(route.query.screeningId)
}

const mine = ref({
  email: '',
  rows: [],
  headers: [
    { title:'ID', key:'id', width:80 },
    { title:'Movie', key:'movie' },
    { title:'Time', key:'time', width:220 },
    { title:'Actions', key:'actions', width:130 }
  ]
})

async function loadMine(){
  const em = (mine.value.email || '').trim()
  if(rules.email(em) !== true) return notify('Enter a valid email', 'error')
  busy.value.mine = true
  try{
    const { data } = await api.get('/tickets')
    mine.value.rows = (data || []).filter(t => (t.customerEmail||'').toLowerCase() === em.toLowerCase())
    if(!mine.value.rows.length) notify('No tickets found for this email','error')
  }catch(e){
    notify('Failed to load tickets','error')
  }finally{
    busy.value.mine = false
  }
}

async function cancel(t){
  try{
    await api.delete(`/tickets/${t.id}`)
    notify('Ticket cancelled')
    mine.value.rows = mine.value.rows.filter(x => x.id !== t.id)
  }catch(e){
    notify('Cancel failed','error')
  }
}

const soldPerMovie = ref([])
function barValue(x){
  const max = Math.max(...soldPerMovie.value.map(s => Number(s.soldTickets)||0), 1)
  return Math.round((Number(x)||0) / max * 100)
}
async function loadSold(){
  try{
    const { data } = await api.get('/tickets/sold-per-movie')
    soldPerMovie.value = data || []
  }catch{ /* ignore */ }
}

onMounted(() => {
  if(route.query.screeningId) prefillFromQuery()
  loadSold()
})
</script>

<style scoped>
.tickets-page{
  background: linear-gradient(180deg, #fff7f5 0%, #ffe9e1 55%, #ffe0d6 100%);
  min-height: 100%;
  padding-bottom: 32px;
}

.hero{
  max-width: 1140px;
  margin: -18px auto 8px;
  padding: 24px 16px 10px;
  text-align: center;
}
.hero-title{
  margin: 0;
  font-weight: 800;
  font-size: clamp(22px, 3.2vw, 36px);
  color: #392e31;
}
.hero-sub{
  margin-top: 6px;
  color: #6e5e63;
  font-size: 0.98rem;
}

.container{ max-width:1140px; margin:0 auto; padding:0 16px; }
.card{
  border: 1px solid rgba(0,0,0,0.06);
  border-radius: 16px;
  background: #fff;
}
.title{ font-weight: 700; color:#2f2529; }
.count{ width: 36px; text-align: right; font-weight: 700; }
</style>
