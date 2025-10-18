import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import './assets/main.css'

import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import '@mdi/font/css/materialdesignicons.css'

const vuetify = createVuetify({
    theme: {
        defaultTheme: 'light',
        themes: {
            light: {
                colors: {
                    background: '#FFFFFF',
                    surface: '#FFFFFF',
                    primary: '#0b7a5b'
                }
            }
        }
    }
})

createApp(App).use(router).use(vuetify).mount('#app')
