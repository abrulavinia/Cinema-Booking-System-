package com.cinemabooking.controller;
import lombok.RequiredArgsConstructor;
import com.cinemabooking.db.Screening;
import com.cinemabooking.services.RecommendationService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/recommendations")
@CrossOrigin(origins = {"http://localhost:8080","http://localhost:5173"})
@RequiredArgsConstructor
public class RecommendationController {

    private final RecommendationService service;
    @GetMapping
    public List<Screening> recommend(@RequestParam String email) {
        return service.recommend(email);
    }
}