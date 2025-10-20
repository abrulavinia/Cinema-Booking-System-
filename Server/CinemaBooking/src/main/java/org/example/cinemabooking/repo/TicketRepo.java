package org.example.cinemabooking.repo;
import org.example.cinemabooking.entity.Ticket;
import org.springframework.data.jpa.repository.*;
import org.example.cinemabooking.TicketSoldPerMovie;
import java.util.List;

public interface TicketRepo extends JpaRepository<Ticket, Long> {


    @Query("""
    select t.screening.movie.id as movieId, count(t) as sold
    from Ticket t
    group by t.screening.movie.id
    order by sold desc
  """)
    List<TicketSoldPerMovie> ticketSoldPerMovie();
}