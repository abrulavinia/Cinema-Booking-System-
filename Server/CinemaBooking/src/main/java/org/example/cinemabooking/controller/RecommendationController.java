package org.example.cinemabooking.controller;

import lombok.RequiredArgsConstructor;
import org.example.cinemabooking.entity.Screening;
import org.example.cinemabooking.repo.ScreeningRepo;
import org.example.cinemabooking.repo.TicketRepo;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.time.LocalDateTime;
import java.util.Comparator;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/api/recommendations")
@RequiredArgsConstructor
public class RecommendationController {
    private final TicketRepo tickets;
    private final ScreeningRepo screenings;

    @GetMapping
    public List<Screening> recommend(@RequestParam String email){
        var favGenres = tickets.findAll().stream()
                .filter(t -> t.getCustomerEmail().equalsIgnoreCase(email))
                .collect(Collectors.groupingBy(t -> t.getScreening().getMovie().getGenre(), Collectors.counting()))
                .entrySet().stream()
                .sorted(Map.Entry.<String,Long>comparingByValue().reversed())
                .map(Map.Entry::getKey).limit(2).toList();

        return screenings.findAll().stream()
                .filter(s -> s.getTime().isAfter(LocalDateTime.now()))
                .filter(s -> favGenres.isEmpty() || favGenres.contains(s.getMovie().getGenre()))
                .sorted(Comparator.comparingInt(Screening::getSeats_sold).reversed()
                        .thenComparing(Screening::getTime))
                .limit(5).toList();
    }
}
