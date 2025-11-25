package com.cinemabooking.repo;

import com.cinemabooking.db.Manager;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface ManagerRepo extends JpaRepository<Manager, Long> {

    Optional<Manager> findByUsernameAndPassword(String username, String password);
}