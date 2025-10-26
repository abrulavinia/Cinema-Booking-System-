package com.cinemabooking.repo;
import com.cinemabooking.db.Movie;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.List;
public interface MovieRepo extends JpaRepository<Movie, Long>{
    List<com.cinemabooking.db.Movie> findByGenreIgnoreCase(String genre);
}
