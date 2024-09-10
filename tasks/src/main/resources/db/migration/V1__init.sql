create table tasks
(
    id          serial primary key,
    name        varchar(255) unique not null,
    description text
)