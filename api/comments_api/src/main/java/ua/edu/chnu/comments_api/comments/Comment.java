package ua.edu.chnu.comments_api.comments;

import lombok.Builder;
import lombok.Data;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Builder
@Data
@Document("comments")
public class Comment {
    @Id
    private String id;
    
    private String content;
    private int courseId;
}