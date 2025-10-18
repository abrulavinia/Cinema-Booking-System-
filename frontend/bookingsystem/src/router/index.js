import { createRouter, createWebHistory } from 'vue-router'
import Dashboard from '@/components/Dashboard.vue'
import MovieList from '@/components/MovieList.vue'
import ScreeningList from '@/components/ScreeningList.vue'
import TicketDesk from '@/components/TicketDesk.vue'
import Recommandation from '@/components/Recommandation.vue'

const routes = [
    { path: '/', component: Dashboard },
    { path: '/movies', component: MovieList },
    { path: '/screenings', component: ScreeningList },
    { path: '/tickets', component: TicketDesk },
    { path: '/recommendations', component: Recommandation},
]

export default createRouter({
    history: createWebHistory(),
    routes,
})
