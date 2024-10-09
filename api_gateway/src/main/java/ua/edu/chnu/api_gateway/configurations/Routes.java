package ua.edu.chnu.api_gateway.configurations;

import org.springframework.cloud.gateway.server.mvc.handler.GatewayRouterFunctions;
import org.springframework.cloud.gateway.server.mvc.handler.HandlerFunctions;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.web.servlet.function.RequestPredicates;
import org.springframework.web.servlet.function.RouterFunction;
import org.springframework.web.servlet.function.ServerResponse;

@Configuration(proxyBeanMethods = false)
public class Routes {
    @Bean
    public RouterFunction<ServerResponse> coursesApiRoute() {
        return GatewayRouterFunctions.route("courses-api")
                .route(RequestPredicates.path("/api/courses"), HandlerFunctions.http(System.getenv("COURSES_API_URL")))
                .build();
    }

    @Bean
    public RouterFunction<ServerResponse> commentsApiRoute() {
        return GatewayRouterFunctions.route("comments-api")
                .route(RequestPredicates.path("/api/comments"), HandlerFunctions.http(System.getenv("COMMENTS_API_URL")))
                .build();
    }
}