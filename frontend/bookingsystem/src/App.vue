<script setup>
import { ref } from 'vue'
import { useRoute } from 'vue-router'

const drawer = ref(false)
const route = useRoute()

const nav = [
  { to: '/', text: 'Dashboard', icon: 'mdi-view-grid-outline' },
  { to: '/movies', text: 'Movies', icon: 'mdi-movie-open-outline' },
  { to: '/screenings', text: 'Screenings', icon: 'mdi-television-play' },
  { to: '/tickets', text: 'Tickets', icon: 'mdi-ticket-confirmation-outline' },
]
</script>

<template>
  <v-app>
    <!-- Top bar -->
    <v-app-bar flat class="app-bar">
      <v-app-bar-nav-icon class="hide-desktop" @click="drawer = !drawer" />
      <v-toolbar-title class="font-weight-bold">
        🎬 Cinema <span class="brand-accent">Booking</span>
      </v-toolbar-title>
      <v-spacer/>
      <div class="hide-mobile">
        <v-btn
            v-for="item in nav"
            :key="item.to"
            :to="item.to"
            variant="text"
            class="mx-1"
            :color="route.path === item.to ? 'primary' : 'default'"
            :prepend-icon="item.icon"
        >{{ item.text }}</v-btn>
      </div>
    </v-app-bar>

    <v-navigation-drawer v-model="drawer" temporary>
      <v-list nav density="comfortable">
        <v-list-item v-for="item in nav" :key="item.to"
                     :to="item.to" :prepend-icon="item.icon"
                     :title="item.text" @click="drawer=false"/>
      </v-list>
    </v-navigation-drawer>

    <div class="hero">
      <div class="hero-inner">
        <h1>Book. Watch. Enjoy.</h1>
        <p class="subtitle">Manage movies, screenings and tickets in a snap.</p>
      </div>
    </div>

    <v-main class="app-main">
      <v-container fluid>
        <div class="page">
          <router-view />
        </div>
      </v-container>
    </v-main>

    <v-footer class="app-footer" app>
      <div class="page small text-medium-emphasis">© {{ new Date().getFullYear() }} Cinema Booking</div>
    </v-footer>
  </v-app>
</template>
