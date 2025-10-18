<script setup>
import { ref, onMounted } from 'vue'
import { api } from '@/services/api'
const screenings = ref([])
const popular = ref([])
const query = ref({ movieId: '', from: '', to: '' })
const loading = ref(false)
const fmt = d => new Date(d).toLocaleString()
async function loadAll(){ loading.value=true; try{ screenings.value=(await api.get('/screenings')).data } finally{ loading.value=false } }
async function loadPopular(){ popular.value=(await api.get('/screenings/popular',{params:{top:5}})).data }
async function search(){
  const p={}
  if(query.value.movieId) p.movieId=query.value.movieId
  if(query.value.from)    p.from=query.value.from
  if(query.value.to)      p.to=query.value.to
  screenings.value=(await api.get('/screenings/search',{params:p})).data
}
onMounted(()=>{ loadAll(); loadPopular() })
</script>

<template>
  <h2 class="mb-4">Screenings</h2>

  <v-card class="mb-6" elevation="1">
    <v-card-text>
      <v-row dense>
        <v-col cols="12" md="3"><v-text-field v-model="query.movieId" label="Movie ID" type="number" variant="outlined" /></v-col>
        <v-col cols="12" md="3"><v-text-field v-model="query.from" label="From (YYYY-MM-DDTHH:mm)" variant="outlined" /></v-col>
        <v-col cols="12" md="3"><v-text-field v-model="query.to" label="To (YYYY-MM-DDTHH:mm)" variant="outlined" /></v-col>
        <v-col cols="12" md="3" class="d-flex align-end">
          <v-btn class="mr-2" @click="search">Search</v-btn>
          <v-btn variant="text" @click="loadAll">Reset</v-btn>
        </v-col>
      </v-row>
    </v-card-text>
  </v-card>

  <v-data-table
      :items="screenings" :loading="loading"
      :headers="[
      { title:'ID', key:'id', width:80 },
      { title:'Movie', key:'movie.title' },
      { title:'Genre', key:'movie.genre', width:180 },
      { title:'Hall', key:'hall', width:120 },
      { title:'Time', key:'time', width:220 },
      { title:'Sold / Total', key:'seats', width:160 }
    ]"
      class="elevation-1"
  >
    <template #item.time="{ item }">{{ fmt(item.time) }}</template>
    <template #item.seats="{ item }">{{ item.seats_sold }} / {{ item.seats_total }}</template>
  </v-data-table>

  <h3 class="mt-8 mb-2">Top popular</h3>
  <v-list density="comfortable" class="elevation-1">
    <v-list-item v-for="s in popular" :key="s.id"
                 :title="`${s.movie.title} — ${s.seats_sold}/${s.seats_total}`"
                 :subtitle="`${s.hall} • ${fmt(s.time)}`" />
  </v-list>
</template>
