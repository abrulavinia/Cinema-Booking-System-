package com.cinemabooking.services;

import com.cinemabooking.db.Screening;
import com.cinemabooking.repo.ScreeningRepo;
import com.cinemabooking.repo.TicketRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.time.LocalDateTime;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@Service
@RequiredArgsConstructor
public class RecommendationService {

    private final TicketRepo tickets;
    private final ScreeningRepo screenings;

    public List<com.cinemabooking.db.Screening> recommend(String email) {

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
