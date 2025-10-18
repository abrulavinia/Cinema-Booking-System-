<template>
  <v-app class="bg-white">
    <v-app-bar flat class="bg-white">
      <v-toolbar-title class="text-h6">Cinema Booking</v-toolbar-title>
      <v-spacer />
      <v-btn to="/" variant="text">Dashboard</v-btn>
      <v-btn to="/movies" variant="text">Movies</v-btn>
      <v-btn to="/screenings" variant="text">Screenings</v-btn>
      <v-btn to="/tickets" variant="text">Tickets</v-btn>
    </v-app-bar>

    <v-main class="bg-white">
      <!-- fluid = 100% lățime; container-max limitează conținutul, nu fundalul -->
      <v-container fluid class="py-6">
        <div class="container-max">
          <router-view />
        </div>
      </v-container>
    </v-main>
  </v-app>
</template>
