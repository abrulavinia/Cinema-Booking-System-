import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import '@mdi/font/css/materialdesignicons.css'
import './assets/main.css'

const vuetify = createVuetify({
    theme: {
        defaultTheme: 'light',
        themes: {
            light: {
                colors: {
                    background: '#ffffff',
                    surface: '#ffffff',
                    primary: '#bb8c8c',
                },
            },
        },
    },
})

createApp(App).use(router).use(vuetify).mount('#app')
