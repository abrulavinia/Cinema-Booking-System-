<template>
  <div class="home">
    <section class="hero">
      <img src="@/assets/Cinema.jpg" alt="Cinema Banner" class="banner-img" />
      <div class="hero-center">
        <h1 class="hero-title">Cinema Booking</h1>
        <p class="hero-subtitle">Book • Watch • Enjoy</p>
      </div>
    </section>

    <section class="container">
      <v-row class="quick-row" dense>
        <v-col cols="12" md="4">
          <v-card class="quick" @click="go('/movies')" role="button">
            <div class="quick-title">Browse Movies</div>
            <div class="quick-sub">Catalog, genres, durations</div>
          </v-card>
        </v-col>

        <v-col cols="12" md="4">
          <v-card class="quick" @click="go('/screenings')" role="button">
            <div class="quick-title">See Screenings</div>
            <div class="quick-sub">Check what's playing now</div>
          </v-card>
        </v-col>

        <v-col cols="12" md="4">
          <v-card class="quick" @click="go('/tickets')" role="button">
            <div class="quick-title">My Tickets</div>
            <div class="quick-sub">View or cancel easily</div>
          </v-card>
        </v-col>
      </v-row>
    </section>

    <section class="container">
      <div class="section-header">
        <h2 class="section-title">Now Playing</h2>
        <v-btn variant="text" size="small" @click="go('/movies')">
          View all
        </v-btn>
      </div>

      <v-row dense>
        <v-col
            v-for="m in nowPlaying"
            :key="m.id"
            cols="12" sm="6" md="4" lg="3"
        >
          <v-card class="movie movie--noimg" elevation="0">
            <div class="movie-body">
              <div class="movie-title" :title="m.title">{{ m.title }}</div>
              <div class="movie-meta">
                <v-chip size="x-small" variant="flat">{{ m.genre }}</v-chip>
                <span class="dot">•</span>
                <span>{{ m.duration }} min</span>
              </div>
              <v-btn
                  size="small"
                  class="mt-2"
                  color="primary"
                  variant="tonal"
                  @click="go('/screenings')"
              >
                Showtimes
              </v-btn>
            </div>
          </v-card>
        </v-col>

        <v-col v-if="!loading.movies && !nowPlaying.length" cols="12">
          <v-alert type="info" variant="tonal">
            No active movies yet.
          </v-alert>
        </v-col>
      </v-row>
    </section>

    <v-snackbar v-model="snackbar.show" :color="snackbar.color" timeout="2200">
      {{ snackbar.text }}
    </v-snackbar>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { api } from '@/services/api'

const router = useRouter()
const loading = ref({ movies: false, popular: false })
const movies  = ref([])
const popular = ref([])
const nowPlaying = ref([])

const snackbar = ref({ show:false, text:'', color:'success' })
const notify = (t, type='success') => (snackbar.value = { show:true, text:t, color: type==='error' ? 'red' : 'green' })
function go(path) { router.push(path) }

async function loadNowPlaying() {
  loading.value.movies = true
  try {
    const {data} = await api.get('/screenings/today')
    const seen = new Set()
    const moviesToday = []

    for (const s of data) {
      if (!seen.has(s.movie.id)) {
        seen.add(s.movie.id)
        moviesToday.push(s.movie)
      }
    }
    nowPlaying.value = moviesToday.slice(0, 8)

  } catch {
    notify('Failed to load now playing','error')
    nowPlaying.value = []
  } finally {
    loading.value.movies = false
  }
}

async function loadPopular() {
  loading.value.popular = true
  try {
    const { data } = await api.get('/screenings/popular', { params: { top: 6 } })
    popular.value = Array.isArray(data) ? data : []
  } finally {
    loading.value.popular = false
  }
}
onMounted(() => {
  loadNowPlaying()
  loadPopular()
})
</script>

<style scoped>
:root {
  --bg-1: #fff7f5;
  --bg-2: #ffe9e1;
  --bg-3: #ffe0d6;
  --ink-1: #2b2630;
}

.home {
  display: flow-root;
  background: linear-gradient(180deg, var(--bg-1) 0%, var(--bg-2) 55%, var(--bg-3) 100%);
  padding-top: 0;
  color: var(--ink-1);
}

.hero {
  max-width: 1100px;
  height: 340px;
  margin: 0 auto 8px;
  border-radius: 18px;
  position: relative;
  overflow: hidden;
  background: #ddd;
}

.banner-img {
  position: absolute;
  inset: 0;
  width: 100%;
  height: 100%;
  object-fit: cover;
  object-position: center 42%;
  display: block;
  filter: none;
  transform: none;
  transition: transform .4s ease;
}
.hero:hover .banner-img { transform: scale(1.02); }

.hero-center{
  position:absolute; inset:0;
  display:grid; place-items:center;
  text-align:center; padding:12px;
}
.hero-title{
  margin:0; font-weight:800;
  font-size:clamp(28px,3.8vw,44px);
  color:#fff;
  text-shadow:0 2px 6px rgba(0,0,0,.35);
}
.hero-subtitle{
  margin-top:6px; letter-spacing:.2em;
  font-size:clamp(12px,1.4vw,14px);
  color:#fff;
  text-shadow:0 1px 4px rgba(0,0,0,.35);
}

.container {
  max-width: 1140px;
  margin: 10px auto 0;
  padding: 0 16px;
}

.quick-row { margin-top: 8px; }
.quick {
  text-align: center;
  padding: 18px 16px;
  background: #fff;
  border: 1px solid rgba(243, 170, 170, 0.20);
  border-radius: 16px;
  transition: transform .2s ease, box-shadow .2s ease, background .2s ease;
}
.quick:hover {
  transform: translateY(-3px);
  box-shadow: 0 10px 24px rgba(243, 170, 170, 0.18);
  background: #fff8f6;
}
.quick-title { font-weight: 700; color: #3b2e34; }
.quick-sub { font-size: .92rem; color: rgba(0, 0, 0, 0.56); }

.movie {
  border-radius: 14px;
  background: #fff;
  border: 1px solid rgba(0,0,0,0.06);
}
.movie--noimg { padding-top: 10px; }
.movie .movie-body { padding: 12px 14px 16px; }
.movie-title {
  font-weight: 700; line-height: 1.2; margin-bottom: 2px;
  white-space: nowrap; overflow: hidden; text-overflow: ellipsis;
  color: #2d2428;
}
.movie-meta { display: flex; align-items: center; gap: 8px; color: #6b5a60; }
.dot { opacity: .5 }

.section-header {
  display: flex; align-items: center; justify-content: space-between;
  margin: 6px 0 10px;
}
.section-title { font-weight: 800; margin: 0; color: #2f2529; }
</style>