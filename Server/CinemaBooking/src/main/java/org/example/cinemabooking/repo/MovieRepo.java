package org.example.cinemabooking.repo;
import org.example.cinemabooking.entity.Movie;
import org.springframework.data.jpa.repository.JpaRepository;
import java.util.List;
public interface MovieRepo extends JpaRepository<Movie, Long>{
    List<Movie> findByGenreIgnoreCase(String genre);
}
