create or replace procedure delete_course_by_id(p_id integer)
    language sql
as
$$
delete
from courses
where id = p_id
$$