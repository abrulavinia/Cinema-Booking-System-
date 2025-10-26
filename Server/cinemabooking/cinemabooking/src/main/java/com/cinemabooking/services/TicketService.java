package com.cinemabooking.services;

import com.cinemabooking.db.TicketStatus;
import jakarta.transaction.Transactional;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.stereotype.Service;
import org.springframework.web.server.ResponseStatusException;

import java.util.List;

@Service
public class TicketService {

    private static com.cinemabooking.repo.TicketRepo tickets;
    private static com.cinemabooking.repo.ScreeningRepo screenings;

    @Autowired
    public TicketService(com.cinemabooking.repo.TicketRepo ticketRepo, com.cinemabooking.repo.ScreeningRepo screeningRepo) {
        tickets = ticketRepo;
        screenings = screeningRepo;
    }

    public static List<com.cinemabooking.db.Ticket> all() {
        return tickets.findAll();
    }

    @Transactional
    public com.cinemabooking.db.Ticket buy(Long screeningId, String email) {
        if (email == null || (email = email.trim()).isEmpty()) {
            throw new ResponseStatusException(HttpStatus.BAD_REQUEST, "Email is required");
        }

        var s = screenings.findById(screeningId)
                .orElseThrow(() -> new ResponseStatusException(HttpStatus.NOT_FOUND, "Screening not found"));

        if (s.getSeats_sold() >= s.getSeats_total())
            throw new ResponseStatusException(HttpStatus.CONFLICT, "No more seats available");

        s.setSeats_sold(s.getSeats_sold() + 1);
        screenings.save(s);

        var t = new com.cinemabooking.db.Ticket();
        t.setScreening(s);
        t.setCustomerEmail(email);
        t.setStatus(com.cinemabooking.db.TicketStatus.RESERVED);
        return tickets.save(t);
    }



    public static void cancel(Long ticketId) {
        com.cinemabooking.db.Ticket t = tickets.findById(ticketId).orElseThrow();
        com.cinemabooking.db.Screening s = t.getScreening();
        s.setSeats_sold(Math.max(0, s.getSeats_sold() - 1));
        screenings.save(s);
        tickets.delete(t);
    }

    public static List<com.cinemabooking.TicketSoldPerMovie> soldPerMovie() {
        return tickets.ticketSoldPerMovie();
    }
}
