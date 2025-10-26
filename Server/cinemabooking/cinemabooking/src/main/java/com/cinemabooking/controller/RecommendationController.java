package com.cinemabooking.controller;
import lombok.RequiredArgsConstructor;
import com.cinemabooking.db.Screening;
import com.cinemabooking.services.RecommendationService;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/recommendations")
@RequiredArgsConstructor
public class RecommendationController {

    @GetMapping
    public List<Screening> recommend(@RequestParam String email) {
        return RecommendationService.recommend(email);
    }
}