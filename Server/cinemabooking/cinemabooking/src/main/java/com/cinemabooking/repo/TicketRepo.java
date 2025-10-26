package com.cinemabooking.repo;

import com.cinemabooking.db.Ticket;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;

public interface TicketRepo extends JpaRepository<Ticket, Long> {
    @Query("""
        SELECT t.screening.movie.title AS title,
               COUNT(t) AS soldTickets
        FROM Ticket t
        GROUP BY t.screening.movie.title
        ORDER BY COUNT(t) DESC
    """)
    List<com.cinemabooking.TicketSoldPerMovie> ticketSoldPerMovie();
}
