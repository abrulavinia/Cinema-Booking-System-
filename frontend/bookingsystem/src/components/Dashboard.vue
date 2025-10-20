<template>
  <div class="page">
    <div class="welcome-card">
      <v-icon color="primary" size="30" class="mr-2">mdi-hand-wave</v-icon>
      <div>
        <h2>Welcome to Cinema Booking!</h2>
        <p>Manage movies, screenings, and tickets all in one place.</p>
      </div>
    </div>
  </div>
</template>

<style scoped>
.welcome-card {
  display: flex;
  align-items: center;
  background: #ffffff;
  border-radius: 16px;
  padding: 28px 32px;
  box-shadow: 0 6px 18px rgba(0,0,0,0.05);
  gap: 16px;
}
.welcome-card h2 {
  margin: 0;
  font-size: 1.5rem;
  font-weight: 700;
  color: #0b7a5b;
}
.welcome-card p {
  margin: 4px 0 0;
  color: #555;
}
</style>
