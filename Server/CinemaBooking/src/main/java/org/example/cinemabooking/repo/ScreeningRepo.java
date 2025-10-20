package org.example.cinemabooking.repo;

import org.example.cinemabooking.entity.Screening;
import org.springframework.data.domain.*;
import org.springframework.data.jpa.repository.*;
import org.springframework.data.repository.query.Param;

import java.time.LocalDateTime;
import java.util.List;

public interface ScreeningRepo extends JpaRepository<Screening, Long> {
    @Query("""
    select s from Screening s
    where (:movieId is null or s.movie.id = :movieId)
      and (:fromTime is null or s.time >= :fromTime)
      and (:toTime   is null or s.time <= :toTime)
    order by s.time
  """)
    List<Screening> search(@Param("movieId") Long movieId,
                           @Param("fromTime") LocalDateTime from,
                           @Param("toTime") LocalDateTime to);


    @Query("select s from Screening s order by s.seats_sold desc, s.time desc")
    Page<Screening> mostPopular(Pageable pageable);
}
