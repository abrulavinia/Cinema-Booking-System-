<script setup>
import { ref } from 'vue'
import { api } from '@/services/api'
const email = ref('')
const recs = ref([])
const loading = ref(false)
const fmt = d => new Date(d).toLocaleString()

async function fetchRecs() {
  if (!email.value) return
  loading.value = true
  try {
    const { data } = await api.get('/recommendations', { params: { email: email.value } })
    recs.value = data
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
          <v-text-field v-model="email" label="Customer email" variant="outlined" />
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
    </v-card-text>
  </v-card>
</template>
