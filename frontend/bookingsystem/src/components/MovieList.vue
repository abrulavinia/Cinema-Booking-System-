<template>
  <div class="movies-page">
    <section class="hero">
      <h1 class="hero-title">Movies</h1>
      <p class="hero-sub">Discover what's playing and pick your next title.</p>
    </section>

    <section class="container">
      <v-card class="filters" elevation="0">
        <v-row dense align="center">
          <v-col cols="12" md="4">
            <v-text-field
                v-model="filters.q"
                variant="outlined"
                label="Search by title"
                clearable
            />
          </v-col>

          <v-col cols="12" sm="6" md="3">
            <v-combobox
                v-model="filters.genre"
                :items="genres"
                label="Genre"
                variant="outlined"
                clearable
                :disabled="!genres.length"
            />
          </v-col>

          <v-col cols="12" sm="6" md="3">
            <v-select
                v-model="filters.sort"
                :items="sortOptions"
                label="Sort by"
                variant="outlined"
            />
          </v-col>

          <v-col cols="12" md="2">
            <v-switch
                v-model="filters.onlyActive"
                inset
                color="pink-lighten-2"
                label="Only ACTIVE"
                class="mt-1"
            />
          </v-col>
        </v-row>
      </v-card>
    </section>

    <section class="container">
      <div v-if="loading" class="grid">
        <v-skeleton-loader
            v-for="i in itemsPerPage"
            :key="'sk'+i"
            type="article"
            class="card"
        />
      </div>

      <template v-else>
        <div v-if="pagedItems.length" class="grid">
          <v-card
              v-for="m in pagedItems"
              :key="m.id"
              class="card card--noimg"
              elevation="0"
          >
            <div class="card-body">
              <div class="top">
                <div class="title" :title="m.title">{{ m.title }}</div>
                <v-chip
                    v-if="m.status && m.status !== 'ACTIVE'"
                    size="x-small"
                    color="deep-orange-lighten-3"
                    class="uppercase"
                >
                  {{ m.status }}
                </v-chip>
              </div>

              <div class="meta">
                <v-chip size="x-small" variant="flat">{{ m.genre }}</v-chip>
                <span class="dot">•</span>
                <span>{{ m.duration }} min</span>
              </div>

              <v-btn
                  size="small"
                  class="mt-3"
                  color="primary"
                  variant="tonal"
                  @click="goToScreenings(m.id)"
              >
                See screenings
              </v-btn>
            </div>
          </v-card>
        </div>

        <v-alert v-else type="info" variant="tonal" class="mt-4">
          No movies found. Try another filter.
        </v-alert>

        <div class="pager" v-if="items.length > itemsPerPage">
          <v-select
              v-model="itemsPerPage"
              :items="[8, 12, 16, 20]"
              label="Items/page"
              variant="outlined"
              style="max-width: 130px"
          />
          <v-pagination
              v-model="page"
              :length="Math.ceil(items.length / itemsPerPage)"
              total-visible="5"
          />
        </div>
      </template>
    </section>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from 'vue'
import { useRouter } from 'vue-router'
import { api } from '@/services/api'

const router = useRouter()

const loading = ref(true)
const allMovies = ref([])

const filters = ref({
  q: '',
  genre: '',
  onlyActive: true,
  sort: 'title-asc',
})

const sortOptions = [
  { title: 'Title (A-Z)', value: 'title-asc' },
  { title: 'Title (Z-A)', value: 'title-desc' },
  { title: 'Duration ↑', value: 'dur-asc' },
  { title: 'Duration ↓', value: 'dur-desc' },
]

const page = ref(1)
const itemsPerPage = ref(12)

const genres = computed(() =>
    [...new Set((allMovies.value || []).map(m => m.genre).filter(Boolean))].sort()
)

const items = computed(() => {
  let arr = [...allMovies.value]

  if (filters.value.onlyActive) {
    arr = arr.filter(m => (m.status || '').toUpperCase() === 'ACTIVE')
  }

  if (filters.value.genre) {
    const g = String(filters.value.genre).toLowerCase()
    arr = arr.filter(m => (m.genre || '').toLowerCase() === g)
  }

  if (filters.value.q) {
    const q = filters.value.q.trim().toLowerCase()
    arr = arr.filter(m => (m.title || '').toLowerCase().includes(q))
  }

  switch (filters.value.sort) {
    case 'title-asc':  arr.sort((a,b) => (a.title||'').localeCompare(b.title||'')); break
    case 'title-desc': arr.sort((a,b) => (b.title||'').localeCompare(a.title||'')); break
    case 'dur-asc':    arr.sort((a,b) => (a.duration||0) - (b.duration||0)); break
    case 'dur-desc':   arr.sort((a,b) => (b.duration||0) - (a.duration||0)); break
  }

  return arr
})

const pagedItems = computed(() => {
  const start = (page.value - 1) * itemsPerPage.value
  return items.value.slice(start, start + itemsPerPage.value)
})

watch(
    [itemsPerPage, () => filters.value.q, () => filters.value.genre, () => filters.value.onlyActive, () => filters.value.sort],
    () => { page.value = 1 }
)

function goToScreenings(movieId) {
  router.push({ name: 'screenings', query: { movieId } })
}

async function load() {
  loading.value = true
  try {
    const { data } = await api.get('/movies')
    allMovies.value = Array.isArray(data) ? data : []
  } catch (e) {
    allMovies.value = []
    console.error('Failed to load movies', e)
  } finally {
    loading.value = false
  }
}

onMounted(load)
</script>

<style scoped>
.movies-page {
  background: linear-gradient(180deg, #fff7f5 0%, #ffe9e1 55%, #ffffff 100%);
  min-height: 100%;
  padding-bottom: 32px;
}

.hero {
  max-width: 1140px;
  margin: -18px auto 8px;
  padding: 24px 16px 10px;
  text-align: center;
}
.hero-title {
  margin: 0;
  font-weight: 800;
  font-size: clamp(22px, 3.2vw, 36px);
  color: #392e31;
}
.hero-sub {
  margin: 6px 0 0;
  color: #6e5e63;
  font-size: 0.98rem;
}

.container {
  max-width: 1140px;
  margin: 0 auto;
  padding: 0 16px;
}

.filters {
  border-radius: 16px;
  background: #fff;
  border: 1px solid rgba(0,0,0,0.06);
  padding: 10px 12px;
}

.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(240px, 1fr));
  gap: 14px;
  margin-top: 12px;
}
.card {
  background: #fff;
  border: 1px solid rgba(0,0,0,0.06);
  border-radius: 16px;
}

.card--noimg {
  padding-top: 12px;
}

.card-body {
  padding: 12px 12px 14px;
}
.top {
  display: flex; align-items: center; justify-content: space-between;
}
.title {
  font-weight: 800;
  line-height: 1.15;
  white-space: nowrap; overflow: hidden; text-overflow: ellipsis;
  color: #2d2428;
}
.uppercase { text-transform: uppercase; }
.meta {
  display: flex; align-items: center; gap: 8px;
  color: #6b5a60;
  margin-top: 4px;
}
.dot { opacity: .55 }

.pager {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 16px;
  margin-top: 16px;
}
</style>
