import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from '@/components/Dashboard.vue'
import MovieList from '@/components/MovieList.vue'
import ScreeningList from '@/components/ScreeningList.vue'
import TicketDesk from '@/components/TicketDesk.vue'
import Recommendation from '@/components/Recommendation.vue'

const routes = [
    { path: '/', name: 'dashboard', component: Dashboard },
    { path: '/movies', name: 'movies', component: MovieList },
    { path: '/screenings', name: 'screenings', component: ScreeningList },
    { path: '/tickets', name: 'tickets', component: TicketDesk },
    { path: '/recommendations', name: 'recommendations', component: Recommendation },
]

export default createRouter({
    history: createWebHistory(),
    routes,
})
