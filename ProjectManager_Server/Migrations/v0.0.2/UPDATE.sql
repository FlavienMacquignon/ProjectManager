BEGIN;

ALTER TABLE data.bug ADD COLUMN IF NOT EXISTS description_id uuid ;
ALTER TABLE data.epic ADD COLUMN IF NOT EXISTS description_id uuid ;
ALTER TABLE data.project ADD COLUMN IF NOT EXISTS description_id uuid ;


ALTER TABLE data.bug
    ADD CONSTRAINT fk_bug_description FOREIGN KEY (description_id) REFERENCES data.description (id);

ALTER TABLE data.epic
    ADD CONSTRAINT fk_epic_description FOREIGN KEY (description_id) REFERENCES data.description (id);

ALTER TABLE data.project
    ADD CONSTRAINT fk_project_description FOREIGN KEY (description_id) REFERENCES data.description (id);

-- COMMIT;
ROLLBACK;