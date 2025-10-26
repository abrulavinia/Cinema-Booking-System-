package com.cinemabooking.services;

import com.cinemabooking.db.Screening;
import com.cinemabooking.repo.ScreeningRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.*;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.List;

@Service
public class ScreeningService {

    private static ScreeningRepo screeningRepo;

    @Autowired
    public ScreeningService(ScreeningRepo repo) {
        ScreeningService.screeningRepo = repo;
    }

    public static com.cinemabooking.db.Screening create(com.cinemabooking.db.Screening s) {
        return screeningRepo.save(s);
    }

    public static List<com.cinemabooking.db.Screening> findAll() {
        return screeningRepo.findAll();
    }

    public static com.cinemabooking.db.Screening findById(Long id) {
        return screeningRepo.findById(id).orElseThrow();
    }

    public static com.cinemabooking.db.Screening update(Long id, com.cinemabooking.db.Screening s) {
        com.cinemabooking.db.Screening db = screeningRepo.findById(id).orElseThrow();
        db.setMovie(s.getMovie());
        db.setHall(s.getHall());
        db.setTime(s.getTime());
        db.setSeats_total(s.getSeats_total());
        db.setSeats_sold(s.getSeats_sold());
        return screeningRepo.save(db);
    }

    public static void delete(Long id) {
        screeningRepo.deleteById(id);
    }

    public static List<com.cinemabooking.db.Screening> search(Long movieId, LocalDateTime from, LocalDateTime to) {
        return screeningRepo.search(movieId, from, to);
    }

    public static Page<com.cinemabooking.db.Screening> mostPopular(int page, int size) {
        return screeningRepo.mostPopular(PageRequest.of(page, size));
    }
}
