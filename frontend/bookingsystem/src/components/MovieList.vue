<script setup>
import { ref, onMounted } from 'vue'
import { api } from '@/services/api'
const movies = ref([])
const loading = ref(false)
const form = ref({ title: '', genre: '', duration: 90 })
async function load(){ loading.value = true; try{ movies.value=(await api.get('/movies')).data } finally{ loading.value=false } }
async function addMovie(){ if(!form.value.title||!form.value.genre) return; await api.post('/movies', form.value); form.value={title:'',genre:'',duration:90}; load() }
async function remove(id){ await api.delete(`/movies/${id}`); load() }
onMounted(load)
</script>

<template>
  <h2 class="mb-4">Movies</h2>

  <v-card class="mb-6" elevation="1">
    <v-card-text>
      <v-row dense>
        <v-col cols="12" md="5"><v-text-field v-model="form.title" label="Title" variant="outlined" /></v-col>
        <v-col cols="12" md="4"><v-text-field v-model="form.genre" label="Genre" variant="outlined" /></v-col>
        <v-col cols="8" md="2"><v-text-field v-model.number="form.duration" label="Duration (min)" type="number" variant="outlined" /></v-col>
        <v-col cols="4" md="1" class="d-flex align-end"><v-btn block @click="addMovie">Add</v-btn></v-col>
      </v-row>
    </v-card-text>
  </v-card>

  <v-data-table
      :items="movies" :loading="loading"
      :headers="[
      { title:'ID', key:'id', width: 80 },
      { title:'Title', key:'title' },
      { title:'Genre', key:'genre', width: 180 },
      { title:'Duration (min)', key:'duration', width: 160 },
      { title:'Actions', key:'actions', width: 140 }
    ]"
      items-per-page="10"
      class="elevation-1"
  >
    <template #item.actions="{ item }">
      <v-btn size="small" color="red" variant="text" @click="remove(item.id)">Delete</v-btn>
    </template>
  </v-data-table>
</template>
