<script setup>
import { ref } from 'vue'
import { api } from '@/services/api'
import Recommendations from '@/components/Recommandation.vue'
const screeningId = ref('')
const email = ref('')
const lastTicket = ref(null)
const busy = ref(false)
const fmt = d => new Date(d).toLocaleString()
async function buy(){
  busy.value=true
  try{
    const { data } = await api.post(`/tickets/buy/${screeningId.value}`, null, { params:{ email: email.value }})
    lastTicket.value = data
  } finally { busy.value=false }
}
</script>

<template>
  <h2 class="mb-4">Tickets</h2>

  <v-card class="mb-6" elevation="1">
    <v-card-text>
      <v-row dense>
        <v-col cols="12" md="3"><v-text-field v-model="screeningId" label="Screening ID" type="number" variant="outlined" /></v-col>
        <v-col cols="12" md="5"><v-text-field v-model="email" label="Email" variant="outlined" /></v-col>
        <v-col cols="12" md="2" class="d-flex align-end"><v-btn :loading="busy" @click="buy" block>Buy</v-btn></v-col>
      </v-row>
      <div v-if="lastTicket" class="mt-4">
        <strong>Purchased:</strong>
        #{{ lastTicket.id }} • {{ lastTicket.screening.movie.title }} • {{ fmt(lastTicket.purchasedAt) }}
      </div>
    </v-card-text>
  </v-card>

  <Recommendations />
</template>
