package com.cinemabooking.services;

import com.cinemabooking.db.Screening;
import com.cinemabooking.db.ScreeningStatus;
import com.cinemabooking.repo.ScreeningRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.*;
import org.springframework.stereotype.Service;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.util.List;

@Service
@RequiredArgsConstructor
public class ScreeningService {

    private final ScreeningRepo screeningRepo;

    public com.cinemabooking.db.Screening create(com.cinemabooking.db.Screening s) {
        if(s.getStatus()==null){
            s.setStatus(ScreeningStatus.SCHEDULED);
        }
        return screeningRepo.save(s);
    }

    public List<com.cinemabooking.db.Screening> findAll() {
        return screeningRepo.findAll();
    }

    public com.cinemabooking.db.Screening findById(Long id) {
        return screeningRepo.findById(id).orElseThrow();
    }

    public com.cinemabooking.db.Screening update(Long id, com.cinemabooking.db.Screening s) {
        com.cinemabooking.db.Screening db = screeningRepo.findById(id).orElseThrow();
        db.setMovie(s.getMovie());
        db.setHall(s.getHall());
        db.setTime(s.getTime());
        db.setSeats_total(s.getSeats_total());
        db.setSeats_sold(s.getSeats_sold());
        return screeningRepo.save(db);
    }

    public void delete(Long id) {
        screeningRepo.deleteById(id);
    }

    public List<com.cinemabooking.db.Screening> search(Long movieId, LocalDateTime from, LocalDateTime to) {
        return screeningRepo.search(movieId, from, to);
    }
    public List<Screening> getToday() {
        LocalDate today = LocalDate.now();
        LocalDateTime start = today.atStartOfDay();
        LocalDateTime end = today.plusDays(1).atStartOfDay();

        return screeningRepo.findToday(
                start,
                end
        );
    }
    public List<com.cinemabooking.db.Screening> getMostPopularScreening(int limit) {
        return screeningRepo.getMostPopularScreening(PageRequest.of(0,limit));
    }
}
