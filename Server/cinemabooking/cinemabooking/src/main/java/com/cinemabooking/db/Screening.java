package com.cinemabooking.db;
import jakarta.persistence.*;
import jakarta.validation.constraints.*;
import lombok.*;
import java.time.LocalDateTime;

@Entity
@Table(name="screening")
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Screening {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Long id;

    @ManyToOne(optional=false)
    @JoinColumn(name="movie_id")
    private Movie movie;

    @NotBlank
    private String hall;

    @NotNull
    private LocalDateTime time;

    @Positive
    private int seats_total=180;

    @Min(0)
    private int seats_sold=0;

    @Enumerated(EnumType.STRING)
    private ScreeningStatus status;
}
