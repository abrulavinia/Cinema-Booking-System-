<script setup>
import { ref, onMounted } from 'vue'
import { api } from '@/services/api'

const screenings = ref([])
const popular = ref([])
const loading = ref(false)
const snackbar = ref({ show:false, text:'', color:'success' })

const query = ref({
  movieId: '',
  from: '',
  to: '',
})

const fmt = d => new Date(d).toLocaleString()

async function loadAll() {
  loading.value = true
  try {
    const { data } = await api.get('/screenings')
    screenings.value = data
  } catch {
    notify('Failed to load screenings', 'error')
  } finally {
    loading.value = false
  }
}

async function loadPopular() {
  try {
    const { data } = await api.get('/screenings/popular', { params: { top: 5 } })
    popular.value = data
  } catch {
  }
}

async function search() {
  const params = {}
  if (query.value.movieId) params.movieId = query.value.movieId
  if (query.value.from) params.from = query.value.from
  if (query.value.to) params.to = query.value.to
  try {
    const { data } = await api.get('/screenings/search', { params })
    screenings.value = data
  } catch {
    notify('Search failed', 'error')
  }
}

function reset() {
  query.value = { movieId: '', from: '', to: '' }
  loadAll()
}

function notify(text, type='success') {
  snackbar.value = { show:true, text, color: type==='error' ? 'red' : 'green' }
}

onMounted(() => { loadAll(); loadPopular() })
</script>

<template>
  <h2 class="mb-4">Screenings</h2>

  <v-card class="mb-6" elevation="1">
    <v-card-text>
      <v-row dense>
        <v-col cols="12" md="3">
          <v-text-field v-model="query.movieId" label="Movie ID" type="number" variant="outlined" />
        </v-col>
        <v-col cols="12" md="3">
          <v-text-field v-model="query.from" label="From" type="datetime-local" variant="outlined" />
        </v-col>
        <v-col cols="12" md="3">
          <v-text-field v-model="query.to" label="To" type="datetime-local" variant="outlined" />
        </v-col>
        <v-col cols="12" md="3" class="d-flex align-end ga-2">
          <v-btn @click="search"><v-icon start>mdi-magnify</v-icon>Search</v-btn>
          <v-btn variant="tonal" @click="reset">Reset</v-btn>
        </v-col>
      </v-row>
    </v-card-text>
  </v-card>

  <v-data-table
      :items="screenings"
      :loading="loading"
      :headers="[
      { title:'ID', key:'id', width:80 },
      { title:'Movie', key:'movie.title' },
      { title:'Genre', key:'movie.genre', width:180 },
      { title:'Hall', key:'hall', width:120 },
      { title:'Time', key:'time', width:220 },
      { title:'Sold / Total', key:'seats', width:160 }
    ]"
      :items-per-page="10"
      item-key="id"
      class="elevation-1"
  >
    <template #loading>
      <div class="text-center pa-6">Loading items…</div>
    </template>

    <template #no-data>
      <v-alert type="info" variant="tonal" class="ma-4">No screenings found.</v-alert>
    </template>

    <template #item.time="{ item }">{{ fmt(item.time) }}</template>
    <template #item.seats="{ item }">{{ item.seats_sold }} / {{ item.seats_total }}</template>
  </v-data-table>

  <h3 class="mt-8 mb-2">Top popular</h3>
  <v-list v-if="popular.length" density="comfortable" class="elevation-1">
    <v-list-item
        v-for="s in popular"
        :key="s.id"
        :title="`${s.movie.title} — ${s.seats_sold}/${s.seats_total}`"
        :subtitle="`${s.hall} • ${fmt(s.time)}`"
    />
  </v-list>
  <div v-else class="text-medium-emphasis">No data available.</div>

  <v-snackbar v-model="snackbar.show" :color="snackbar.color" timeout="2000">
    {{ snackbar.text }}
  </v-snackbar>
</template>
