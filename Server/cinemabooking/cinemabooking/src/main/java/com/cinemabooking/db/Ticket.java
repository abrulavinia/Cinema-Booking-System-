package com.cinemabooking.db;
import jakarta.persistence.*;
import jakarta.persistence.Table;
import jakarta.validation.constraints.*;
import lombok.*;
import org.hibernate.annotations.*;
import java.time.LocalDateTime;

@Entity
@Data
@NoArgsConstructor
@AllArgsConstructor
@Table(name="ticket")
public class Ticket {
    @Id
    @GeneratedValue(strategy=GenerationType.IDENTITY)
    private Long id;

    @ManyToOne(optional=false)
    @JoinColumn(name="screening_id")
    private Screening screening;

    @Email
    @NotBlank
    @Column(name="customer_email")
    private String customerEmail;

    @CreationTimestamp
    @Column(name="purchased_at")
    private LocalDateTime purchasedAt;

    @Enumerated(EnumType.STRING)
    @Column(nullable=false)
    private TicketStatus status;

    @PrePersist
    void prePersist() {
        if(status==null){
            status=TicketStatus.RESERVED;
        }
    }
}
