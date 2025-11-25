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
    where s.time >= CURRENT_TIMESTAMP
      and s.status = com.cinemabooking.db.ScreeningStatus.SCHEDULED
      and s.seats_total > 0
    order by (1.0 * s.seats_sold / s.seats_total) desc, s.time asc
    """)
    List<Screening> getMostPopularScreening(Pageable pageable);

    @Query("""
       select s
       from Screening s
       left join Ticket t
         on t.screening = s
        and (t.status is null or t.status <> com.cinemabooking.db.TicketStatus.CANCELLED)
       where s.time >= :start
         and s.time < :end
         and s.status in (
              com.cinemabooking.db.ScreeningStatus.SCHEDULED,
              com.cinemabooking.db.ScreeningStatus.ONGOING
         )
       group by s
       order by count(t) desc
    """)
    List<Screening> findToday(@Param("start") LocalDateTime start,
                              @Param("end") LocalDateTime end);
}
