create or replace procedure update_course_by_id(p_id integer, p_name varchar, p_description text)
    language sql
as
$$
update courses
set name        = p_name,
    description = p_description
where id = p_id
$$