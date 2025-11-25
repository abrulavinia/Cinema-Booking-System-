package com.cinemabooking.controller;

import com.cinemabooking.db.Manager;
import com.cinemabooking.services.ManagerService;
import lombok.RequiredArgsConstructor;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.Map;

@RestController
@RequestMapping("/api/manager")
@RequiredArgsConstructor
@CrossOrigin(origins = {"http://localhost:8080", "http://localhost:5173"})
public class ManagerController {

    private final ManagerService managerService;

    @PostMapping("/login")
    public ResponseEntity<Manager> login(@RequestBody Map<String, String> body) {
        String username = body.get("username");
        String password = body.get("password");

        Manager manager = managerService.login(username, password);
        if (manager == null) {
            return ResponseEntity.status(401).build();
        }
        manager.setPassword(null);
        return ResponseEntity.ok(manager);
    }
}
