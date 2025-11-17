package com.cinemabooking.controller;

import com.cinemabooking.services.TicketService;
import lombok.RequiredArgsConstructor;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/tickets")
@RequiredArgsConstructor
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5173"})
public class TicketController {
    private final TicketService service;

    @GetMapping
    public List<com.cinemabooking.db.Ticket> all() {
        return service.all();
    }

    @PostMapping("/buy/{screeningId}")
    public com.cinemabooking.db.Ticket buy(@PathVariable Long screeningId,
                                           @RequestParam("email") String email) {
        return service.buy(screeningId, email == null ? null : email.trim());
    }

    @DeleteMapping("/{id}")
    public void cancel(@PathVariable Long id) {
        service.cancel(id);
    }

    @GetMapping("/sold-per-movie")
    public List<com.cinemabooking.TicketSoldPerMovie> soldPerMovie() {
        return service.getTicketsSoldPerMovie();
    }
}
