package com.cinemabooking.services;

import com.cinemabooking.db.Screening;
import com.cinemabooking.repo.ScreeningRepo;
import com.cinemabooking.repo.TicketRepo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@Service
public class RecommendationService {

    private static TicketRepo tickets;
    private static ScreeningRepo screenings;

    @Autowired
    public RecommendationService(com.cinemabooking.repo.TicketRepo tRepo, com.cinemabooking.repo.ScreeningRepo sRepo) {
        tickets = tRepo;
        screenings = sRepo;
    }

    public static List<com.cinemabooking.db.Screening> recommend(String email) {

        var favGenres = tickets.findAll().stream()
                .filter(t -> t.getCustomerEmail().equalsIgnoreCase(email))
                .collect(Collectors.groupingBy(
                        t -> t.getScreening().getMovie().getGenre(),
                        Collectors.counting()))
                .entrySet().stream()
                .sorted(Map.Entry.<String, Long>comparingByValue().reversed())
                .map(Map.Entry::getKey)
                .limit(2)
                .toList();

        return screenings.findAll().stream()
                .filter(s -> s.getTime().isAfter(LocalDateTime.now()))
                .filter(s -> favGenres.isEmpty() || favGenres.contains(s.getMovie().getGenre()))
                .sorted(Comparator
                        .comparingInt(Screening::getSeats_sold).reversed()
                        .thenComparing(Screening::getTime))
                .limit(5)
                .toList();
    }
}
