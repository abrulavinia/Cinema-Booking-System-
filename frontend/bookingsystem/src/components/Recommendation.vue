<script setup>
import { ref, watch } from 'vue'
import { api } from '@/services/api'

const props = defineProps({
  prefillEmail: { type: String, default: '' }
})

const email = ref('')
const recs = ref([])
const loading = ref(false)
const snackbar = ref({ show:false, text:'', color:'success' })
const fmt = d => new Date(d).toLocaleString()

watch(() => props.prefillEmail, v => {
  if (v && !email.value) email.value = v
}, { immediate: true })

function notify(text, type='success'){ snackbar.value = { show:true, text, color: type==='error' ? 'red' : 'green' } }

async function fetchRecs(){
  const e = (email.value || '').trim().toLowerCase()
  if (!e) return notify('Please enter an email','error')
  loading.value = true
  try{
    const { data } = await api.get('/recommendations', { params: { email: e } })
    recs.value = data
    if (!recs.value?.length) notify('No recommendations for this email yet.', 'orange')
  } catch (err){
    notify(err?.response?.data?.message || 'Failed to load recommendations', 'error')
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <v-card elevation="1">
    <v-card-title>Recommendations</v-card-title>
    <v-card-text>
      <v-row dense>
        <v-col cols="12" md="6">
          <v-text-field v-model="email" label="Customer email" variant="outlined"/>
        </v-col>
        <v-col cols="12" md="3" class="d-flex align-end">
          <v-btn :loading="loading" @click="fetchRecs">Load</v-btn>
        </v-col>
      </v-row>

      <v-list v-if="recs.length">
        <v-list-item
            v-for="s in recs"
            :key="s.id"
            :title="`${s.movie.title} — ${s.movie.genre}`"
            :subtitle="`${s.hall} • ${fmt(s.time)} • ${s.seats_sold}/${s.seats_total}`"
        />
      </v-list>
      <div v-else class="text-medium-emphasis">No data yet.</div>

      <v-snackbar v-model="snackbar.show" :color="snackbar.color" timeout="2200">
        {{ snackbar.text }}
      </v-snackbar>
    </v-card-text>
  </v-card>
</template>
