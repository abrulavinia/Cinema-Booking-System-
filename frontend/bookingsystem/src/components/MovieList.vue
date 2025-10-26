<script setup>
import { ref, onMounted } from 'vue'
import { api } from '@/services/api'

const movies = ref([])
const loading = ref(false)
const snackbar = ref({ show: false, text: '', color: 'success' })
const form = ref({ title: '', genre: '', duration: 90 })
const formValid = ref(false)
const rules = {
  req: v => !!v || 'Required',
  pos: v => Number(v) > 0 || 'Must be > 0',
}

async function load() {
  loading.value = true
  try {
    const { data } = await api.get('/movies')
    movies.value = data
  } catch (e) {
    notify('Failed to load movies', 'error')
  } finally {
    loading.value = false
  }
}

async function addMovie() {
  if (!formValid.value) return
  try {
    const payload = {
      title: form.value.title,
      genre: form.value.genre,
      duration: Number(form.value.duration)
    }
    await api.post('/movies', payload)
    notify('Movie added successfully')
    form.value = { title: '', genre: '', duration: 90 }
    await load()
  } catch (e) {
    console.error(e)
    notify(e?.response?.data?.message || 'Failed to add movie', 'error')
  }
}


async function remove(id) {
  try {
    await api.delete(`/movies/${id}`)
    notify('Movie deleted')
    await load()
  } catch (e) {
    notify('Failed to delete movie', 'error')
  }
}

function notify(text, type = 'success') {
  snackbar.value = { show: true, text, color: type === 'error' ? 'red' : 'green' }
}

onMounted(load)
</script>

<template>
  <h2 class="mb-4">Movies</h2>

  <v-card class="mb-6" elevation="1">
    <v-card-text>
      <v-form v-model="formValid" @submit.prevent="addMovie">
        <v-row dense>
          <v-col cols="12" md="5">
            <v-text-field v-model="form.title" :rules="[rules.req]" label="Title" variant="outlined" />
          </v-col>
          <v-col cols="12" md="4">
            <v-text-field v-model="form.genre" :rules="[rules.req]" label="Genre" variant="outlined" />
          </v-col>
          <v-col cols="8" md="2">
            <v-text-field v-model.number="form.duration" :rules="[rules.pos]" label="Duration (min)" type="number" variant="outlined" />
          </v-col>
          <v-col cols="4" md="1" class="d-flex align-end">
            <v-btn block type="submit">Add</v-btn>
          </v-col>
        </v-row>
      </v-form>
    </v-card-text>
  </v-card>

  <v-data-table
      :loading="loading"
      :items="movies"
      :headers="[
      { title:'ID', key:'id', width: 80 },
      { title:'Title', key:'title' },
      { title:'Genre', key:'genre', width: 180 },
      { title:'Duration (min)', key:'duration', width: 160 },
      { title:'Actions', key:'actions', width: 140 }
    ]"
      :items-per-page="10"
      item-key="id"
      class="elevation-1"
  >
    <template #loading>
      <div class="text-center pa-6">Loading items…</div>
    </template>

    <template #no-data>
      <v-alert type="info" variant="tonal" class="ma-4">No movies yet. Add your first movie above.</v-alert>
    </template>

    <template #item.actions="{ item }">
      <v-btn size="small" color="red" variant="text" @click="remove(item.id)">
        <v-icon start>mdi-delete-outline</v-icon> Delete
      </v-btn>
    </template>
  </v-data-table>

  <v-snackbar v-model="snackbar.show" :color="snackbar.color" timeout="2000">
    {{ snackbar.text }}
  </v-snackbar>
</template>
