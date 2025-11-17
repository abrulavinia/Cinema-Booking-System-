package com.cinemabooking.repo;

import com.cinemabooking.db.Screening;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.time.LocalDateTime;
import java.util.List;

public interface ScreeningRepo extends JpaRepository<Screening, Long> {
    @Query("""
        SELECT s FROM Screening s
        WHERE (:movieId IS NULL OR s.movie.id = :movieId)
        AND (:fromTime IS NULL OR s.time >= :fromTime)
        AND (:toTime IS NULL OR s.time <= :toTime)
        ORDER BY s.time
  """)
    List<Screening> search(@Param("movieId") Long movieId,
                           @Param("fromTime") LocalDateTime from,
                           @Param("toTime") LocalDateTime to);


    @Query("""
       select s
       from Screening s
       left join Ticket t
         on t.screening = s
        and (t.status is null or t.status <> com.cinemabooking.db.TicketStatus.CANCELLED)
       where s.time >= CURRENT_TIMESTAMP
       group by s
       order by count(t.id) desc, s.time asc
       """)
    List<Screening> getMostPopularScreening(Pageable pageable);
}
