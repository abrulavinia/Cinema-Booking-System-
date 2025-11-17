package com.cinemabooking.repo;

import com.cinemabooking.db.Ticket;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;

import java.util.List;

public interface TicketRepo extends JpaRepository<Ticket, Long> {
    @Query("""
    select m.title as title, count(t.id) as soldTickets
    from Ticket t
      join t.screening s
      join s.movie m
    where (t.status is null or t.status <> com.cinemabooking.db.TicketStatus.CANCELLED)
    group by m.id, m.title
    order by count(t.id) desc
""")
    List<com.cinemabooking.TicketSoldPerMovie> getTicketsSoldPerMovie();
}
