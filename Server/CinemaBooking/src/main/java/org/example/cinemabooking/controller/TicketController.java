package org.example.cinemabooking.controller;

import lombok.RequiredArgsConstructor;
import org.example.cinemabooking.TicketSoldPerMovie;
import org.example.cinemabooking.entity.Screening;
import org.example.cinemabooking.entity.Ticket;
import org.example.cinemabooking.repo.ScreeningRepo;
import org.example.cinemabooking.repo.TicketRepo;
import org.springframework.http.HttpStatus;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@RestController
@RequestMapping("/api/tickets")
@RequiredArgsConstructor
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5173"})
public class TicketController {
    private final TicketRepo tickets;
    private final ScreeningRepo screenings;

    @GetMapping
    public List<Ticket> all(){ return tickets.findAll(); }

    @PostMapping("/buy/{screeningId}")
    public Ticket buy(@PathVariable Long screeningId, @RequestParam String email){
        Screening s = screenings.findById(screeningId).orElseThrow();
        if (s.getSeats_sold() >= s.getSeats_total())
            throw new ResponseStatusException(HttpStatus.CONFLICT, "Sold out");
        s.setSeats_sold(s.getSeats_sold()+1); screenings.save(s);
        return tickets.save(new Ticket(null, s, email, null));
    }

    @DeleteMapping("/{id}")
    public void cancel(@PathVariable Long id){
        Ticket t = tickets.findById(id).orElseThrow();
        Screening s = t.getScreening();
        s.setSeats_sold(Math.max(0, s.getSeats_sold()-1)); screenings.save(s);
        tickets.delete(t);
    }

    @GetMapping("/sold-per-movie")
    public List<TicketSoldPerMovie> soldPerMovie(){ return tickets.ticketSoldPerMovie(); }
}
