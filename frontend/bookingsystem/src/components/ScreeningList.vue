<template>
  <div class="screenings-page">
    <section class="hero">
      <h1 class="hero-title">Screenings</h1>
      <p class="hero-sub">Find showtimes and book your seat.</p>
    </section>

    <section class="container">
      <v-card class="filters" elevation="0">
        <v-row dense align="end">
          <v-col cols="12" md="3">
            <v-text-field
                v-model="filters.movieId"
                type="number"
                label="Movie ID (optional)"
                variant="outlined"
                clearable
            />
          </v-col>

          <v-col cols="12" sm="6" md="3">
            <v-text-field
                v-model="filters.from"
                label="From"
                type="datetime-local"
                variant="outlined"
                clearable
            />
          </v-col>

          <v-col cols="12" sm="6" md="3">
            <v-text-field
                v-model="filters.to"
                label="To"
                type="datetime-local"
                variant="outlined"
                clearable
            />
          </v-col>

          <v-col cols="12" md="3" class="d-flex ga-2">
            <v-btn @click="search" :loading="loading.table" class="flex-grow-1">
              Search
            </v-btn>
            <v-btn variant="tonal" class="flex-grow-1" @click="reset">
              Reset
            </v-btn>
          </v-col>
        </v-row>
      </v-card>
    </section>

    <section class="container">
      <v-data-table
          :items="rows"
          :headers="headers"
          item-key="id"
          :loading="loading.table"
          :items-per-page="10"
          class="elevation-0 table"
      >
        <template #loading>
          <div class="text-center pa-6">Loading…</div>
        </template>

        <template #no-data>
          <v-alert type="info" variant="tonal" class="ma-4">
            No screenings found.
          </v-alert>
        </template>

        <template #item.time="{ item }">
          {{ fmt(item.time) }}
        </template>

        <template #item.sold="{ item }">
          {{ item.seats_sold }} / {{ item.seats_total }}
        </template>

        <template #item.actions="{ item }">
          <v-btn size="small" color="primary" variant="tonal" @click="buy(item)">
             Buy
          </v-btn>
        </template>
      </v-data-table>
    </section>

    <section class="container">
      <div class="section-header">
        <h2 class="section-title">Top popular</h2>
      </div>

      <v-card class="card" elevation="0">
        <v-list v-if="popular.length" density="comfortable">
          <v-list-item
              v-for="s in popular"
              :key="s.id"
              :title="`${s.movie.title} — ${s.hall}`"
              :subtitle="fmt(s.time)"
          >
            <template #append>
              <div class="sold-wrap">
                <div class="sold-text">{{ s.seats_sold }} / {{ s.seats_total }}</div>
                <v-progress-linear
                    :model-value="(s.seats_sold / Math.max(1, s.seats_total)) * 100"
                    height="8"
                    rounded
                />
              </div>
            </template>
          </v-list-item>
        </v-list>
        <v-card-text v-else class="text-medium-emphasis">
          No data available.
        </v-card-text>
      </v-card>
    </section>

    <v-snackbar v-model="snackbar.show" :color="snackbar.color" timeout="2200">
      {{ snackbar.text }}
    </v-snackbar>
  </div>
</template>

<script setup>
import { ref, onMounted, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { api } from '@/services/api'

const route = useRoute()
const router = useRouter()

const loading = ref({ table: false, popular: false })
const rows = ref([])
const popular = ref([])

const headers = [
  { title: 'ID', key: 'id', width: 80 },
  { title: 'Movie', key: 'movie.title' },
  { title: 'Genre', key: 'movie.genre', width: 140 },
  { title: 'Hall', key: 'hall', width: 120 },
  { title: 'Time', key: 'time', width: 220 },
  { title: 'Sold / Total', key: 'sold', width: 140 },
  { title: 'Actions', key: 'actions', width: 120 },
]

const filters = ref({
  movieId: route.query.movieId ? Number(route.query.movieId) : '',
  from: '',
  to: ''
})

const snackbar = ref({ show:false, text:'', color:'success' })
const notify = (t, type='success') => snackbar.value = { show:true, text:t, color: type==='error' ? 'red' : 'green' }

const fmt = (d) => new Date(d).toLocaleString()

function toIsoLocal(dtStr){
  if(!dtStr) return undefined
  const d = new Date(dtStr)
  d.setMinutes(d.getMinutes() - d.getTimezoneOffset())
  return d.toISOString().slice(0,19)   // yyyy-MM-ddTHH:mm:ss (fără Z)
}

async function search(){
  loading.value.table = true
  try{
    const params = {}
    if (filters.value.movieId) params.movieId = filters.value.movieId
    if (filters.value.from)    params.from    = toIsoLocal(filters.value.from)
    if (filters.value.to)      params.to      = toIsoLocal(filters.value.to)

    const { data } = Object.keys(params).length
        ? await api.get('/screenings/search', { params })
        : await api.get('/screenings')

    rows.value = data || []
  } catch(e){
    notify('Failed to load screenings', 'error')
    rows.value = []
  } finally {
    loading.value.table = false
  }
}

function reset(){
  filters.value = { movieId:'', from:'', to:'' }
  search()
}

async function loadPopular(){
  loading.value.popular = true
  try{
    const { data } = await api.get('/screenings/popular', { params: { top: 6 } })
    popular.value = data || []
  } finally {
    loading.value.popular = false
  }
}

function buy(s){
  router.push({ name: 'tickets', query: { screeningId: s.id } })
}

watch(() => route.query.movieId, (v) => {
  if (v) {
    filters.value.movieId = Number(v)
    search()
  }
})

onMounted(() => { search(); loadPopular() })
</script>

<style scoped>
.screenings-page{
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

.container{
  max-width: 1140px;
  margin: 0 auto;
  padding: 0 16px;
}

.filters{
  border-radius: 16px;
  background: #fff;
  border: 1px solid rgba(0,0,0,0.06);
  padding: 12px;
}

.table{
  border: 1px solid rgba(0,0,0,0.06);
  border-radius: 16px;
  overflow: hidden;
  background: #fff;
}

.section-header{ margin: 12px 0 10px; }
.section-title{ margin: 0; font-weight: 800; color: #2f2529; }

.card{
  border: 1px solid rgba(0,0,0,0.06);
  border-radius: 16px;
  background: #fff;
}

.sold-wrap{ width: 180px; display: grid; gap: 6px; }
.sold-text{ font-size: .85rem; color: #6a7082; text-align: right; }
</style>
