package com.cinemabooking.services;

import com.cinemabooking.db.Manager;
import com.cinemabooking.repo.ManagerRepo;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class ManagerService {

    private final ManagerRepo managerRepo;
    public Manager login(String username, String password) {
        if (username == null || password == null) return null;
        return managerRepo
                .findByUsernameAndPassword(username.trim(), password.trim())
                .orElse(null);
    }
}
