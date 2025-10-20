<script setup>
import { ref } from 'vue'
import { api } from '@/services/api'
import Recommendation from '@/components/Recommendation.vue'

const screeningId = ref('')
const email = ref('')
const lastTicket = ref(null)
const busy = ref(false)
const snackbar = ref({ show:false, text:'', color:'success' })
const fmt = d => new Date(d).toLocaleString()

async function buy() {
  if (!screeningId.value || !email.value) return
  busy.value = true
  try {
    const { data } = await api.post(`/tickets/buy/${screeningId.value}`, null, { params: { email: email.value } })
    lastTicket.value = data
    notify('Ticket purchased')
  } catch (e) {
    notify(e?.response?.data?.error || 'Purchase failed', 'error')
  } finally {
    busy.value = false
  }
}

function notify(text, type='success') {
  snackbar.value = { show:true, text, color: type==='error' ? 'red' : 'green' }
}
</script>

<template>
  <h2 class="mb-4">Tickets</h2>

  <v-card class="mb-6" elevation="1">
    <v-card-text>
      <v-row dense>
        <v-col cols="12" md="3">
          <v-text-field v-model="screeningId" label="Screening ID" type="number" variant="outlined" />
        </v-col>
        <v-col cols="12" md="5">
          <v-text-field v-model="email" label="Email" variant="outlined" />
        </v-col>
        <v-col cols="12" md="2" class="d-flex align-end">
          <v-btn :loading="busy" @click="buy" block>
            <v-icon start>mdi-cart</v-icon> Buy
          </v-btn>
        </v-col>
      </v-row>

      <div v-if="lastTicket" class="mt-4">
        <strong>Purchased:</strong>
        #{{ lastTicket.id }} • {{ lastTicket.screening.movie.title }} • {{ fmt(lastTicket.purchasedAt) }}
      </div>
    </v-card-text>
  </v-card>

  <Recommendation />

  <v-snackbar v-model="snackbar.show" :color="snackbar.color" timeout="2000">
    {{ snackbar.text }}
  </v-snackbar>
</template>
