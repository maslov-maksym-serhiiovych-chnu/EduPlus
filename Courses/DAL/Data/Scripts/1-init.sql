create table if not exists courses
(
    id          serial primary key,
    name        varchar(255) unique not null,
    description text
);

create table if not exists course_users
(
    id        serial primary key,
    course_id integer references courses (id) not null
)