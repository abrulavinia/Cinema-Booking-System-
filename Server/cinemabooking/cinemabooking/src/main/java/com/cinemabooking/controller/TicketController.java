package com.cinemabooking.controller;

import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/tickets")
@RequiredArgsConstructor
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5173"})
public class TicketController {
    private final com.cinemabooking.services.TicketService ticketService;
    @GetMapping
    public List<com.cinemabooking.db.Ticket> all() {
        return com.cinemabooking.services.TicketService.all();
    }

    @PostMapping("/buy/{screeningId}")
    public com.cinemabooking.db.Ticket buy(@PathVariable Long screeningId,
                                           @RequestParam("email") String email) {
        return ticketService.buy(screeningId, email == null ? null : email.trim());
    }

    @DeleteMapping("/{id}")
    public void cancel(@PathVariable Long id) {
        com.cinemabooking.services.TicketService.cancel(id);
    }

    @GetMapping("/sold-per-movie")
    public List<com.cinemabooking.TicketSoldPerMovie> soldPerMovie() {
        return com.cinemabooking.services.TicketService.soldPerMovie();
    }
}
