BEGIN;
-- Ensure that the BDD can generate UUID on insert
CREATE
    EXTENSION IF NOT EXISTS "uuid-ossp";
-- Creating default schema
CREATE SCHEMA IF NOT EXISTS data;
CREATE SCHEMA IF NOT EXISTS user_land;

-- Creating Table Bug
CREATE TABLE IF NOT EXISTS data.bug
(
    id
                   UUID
        PRIMARY
            KEY
                        NOT
                            NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    description_id UUID NOT NULL,
    display_id     INTEGER,
    created_at     TIMESTAMP,
    project_id     UUID NOT NULL,
    epic_id        UUID,
    reporter_id    UUID NOT NULL,
    assignated_id  UUID,
    closed_at      TIMESTAMP
);

-- Creating Table Epic
CREATE TABLE IF NOT EXISTS data.epic
(
    id
                   UUID
        PRIMARY
            KEY
                        NOT
                            NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    description_id UUID NOT NULL,
    reporter_id    UUID NOT NULL,
    project_id     UUID NOT NULL,
    assignated_id  UUID
);

-- Creating junction table that link bugs to epic
CREATE TABLE IF NOT EXISTS data.epic_bug
(
    id
            UUID
        PRIMARY
            KEY
                 NOT
                     NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    epic_id UUID NOT NULL,
    bug_id  UUID NOT NULL
);

-- Create Table Project
CREATE TABLE IF NOT EXISTS data.project
(
    id
                       UUID
        PRIMARY
            KEY
                            NOT
                                NULL
                               DEFAULT
                                   uuid_generate_v4
                                   (
                                   ),
    description_id     UUID NOT NULL,
    reported_id        UUID NOT NULL,
    assignated_id      UUID,
    assignated_team_id UUID,
    is_archived        BOOLEAN DEFAULT false
);


CREATE TABLE IF NOT EXISTS data.epic_project
(
    id
               UUID
        PRIMARY
            KEY
                    NOT
                        NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    epic_id    UUID NOT NULL,
    project_id UUID NOT NULL
);

CREATE TABLE IF NOT EXISTS data.project_bug
(
    id
               UUID
        PRIMARY
            KEY
                    NOT
                        NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    bug_id     UUID NOT NULL,
    project_id UUID NOT NULL
);

-- Create Description table
CREATE TABLE IF NOT EXISTS data.description
(
    id
            UUID
        PRIMARY
            KEY
                        NOT
                            NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    title   varchar(50) NOT NULL,
    content varchar(500)
);

-- Create junction table

-- Create Table User
CREATE TABLE IF NOT EXISTS user_land.user
(
    id
               UUID
        PRIMARY
            KEY
                            NOT
                                NULL
                             DEFAULT
                                 uuid_generate_v4
                                 (
                                 ),
    first_name varchar(25)  NOT NULL,
    last_name  varchar(50)  NOT NULL,
    email      varchar(254) NOT NULL,
    icon       varchar(5000) DEFAULT 'data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEAAAABACAYAAACqaXHeAAAACXBIWXMAAAsTAAALEwEAmpwYAAACCElEQVR4nO2Zz0obURTGf2OLpi/QuBI3Lly22gdw0624skEUQaHpSlqDfQHFVWteQoX2Abop9RF04cptEkFREFMoYmvKhTMg004wOndyz3h+8EGYG5L7fffm/jkBwzAMwzAMwzAMwzAMw/BNBEwAc8B7kXv9UtoKyzBQB1pAJ0WubQsoUyAiYA342cV4Um2gVoQZUQJ2ezCe1I58hkoiYPsB5mN91ToT1jIwH2sVZTwHLjMMoC2LqBrqGZqP9RklDADHHgJoaVkLJj2Yj+UOS8Ez5zGACgqoeQxAxW6w6jGADyig4jGAWRQw4TGAFyggAhoezDe1bIPIlTbrAD7xyI/CZZRRyzAAVzV6tNfhL5p++0lKUtS4r/ltzQWRmEgOR+0ejF/KtFc78v+jLFfaZhfjDVnt3SJaWCI50FRulcUr8qxQI24Yxj+U5LL0BqgCH0VVeebahigQEfAK2AAOgOs7bIHuPfvAupTXVC6Mg8ACcJjBSfAIWAGeoYBIaoLd/gC9r5qh1wRHgT0PxpP6AYwQGFPASQ7mY50BrwmEt8DvHM3Hct+53G/zS8CfPpiPdQO865f5GelAp89yAzCdt/kx4CIA87evz+N5mX8iB5VOYHJ9eppHAPMBmE3TYh4BfAvAaJq+5xHAaQBG03SeRwC/AjCaJtc372wCVwGYTepKbp2GYXBn/gIcmHnJ2f8WNwAAAABJRU5ErkJggg==',
    team_id    UUID
);

-- Create table Roles
CREATE TABLE IF NOT EXISTS user_land.roles
(
    id
         UUID
        PRIMARY
            KEY
        NOT
            NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    name varchar(50)
);

-- Create table Team
CREATE TABLE IF NOT EXISTS user_land.team
(
    id
                 UUID
        PRIMARY
            KEY
                             NOT
                                 NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    name         varchar(50) NOT NULL,
    default_role UUID
);

-- Creating join table that link users to roles
CREATE TABLE IF NOT EXISTS user_land.users_roles
(
    id
            UUID
        PRIMARY
            KEY
                 NOT
                     NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    user_id UUID NOT NULL,
    role_id UUID NOT NULL
);

-- Creating join table that link users to team
CREATE TABLE IF NOT EXISTS user_land.users_team
(
    id
            UUID
        PRIMARY
            KEY
                 NOT
                     NULL
        DEFAULT
            uuid_generate_v4
            (
            ),
    user_id UUID NOT NULL,
    team_id UUID NOT NULL
);

-- FOREIGN KEY
-- Bug
ALTER TABLE data.bug
    ADD CONSTRAINT fk_bug_project FOREIGN KEY (project_id) REFERENCES data.project (id);
ALTER TABLE data.bug
    ADD CONSTRAINT fk_bug_epic FOREIGN KEY (epic_id) REFERENCES data.epic (id);
ALTER TABLE data.bug
    ADD CONSTRAINT fk_bug_reporter_user FOREIGN KEY (reporter_id) REFERENCES user_land.user (id);
ALTER TABLE data.bug
    ADD CONSTRAINT fk_bug_assignated_user FOREIGN KEY (assignated_id) REFERENCES user_land.user (id);
ALTER TABLE data.bug
    ADD CONSTRAINT fk_bug_description FOREIGN KEY (description_id) REFERENCES data.description (id);

-- Epic_Project
ALTER TABLE data.epic_project
    ADD CONSTRAINT fk_epic_project FOREIGN KEY (epic_id) REFERENCES data.epic (id);
ALTER TABLE data.epic_project
    ADD CONSTRAINT fk_project_epic FOREIGN KEY (project_id) REFERENCES data.project (id);

-- Project_Bug
ALTER TABLE data.project_bug
    ADD CONSTRAINT fk_project_bug FOREIGN KEY (project_id) REFERENCES data.project (id);
ALTER TABLE data.project_bug
    ADD CONSTRAINT fk_bug_project FOREIGN KEY (bug_id) REFERENCES data.bug (id);

-- Epic
ALTER TABLE data.epic
    ADD CONSTRAINT fk_epic_reported_user FOREIGN KEY (reporter_id) REFERENCES user_land.user (id);
ALTER TABLE data.epic
    ADD CONSTRAINT fk_epic_assignated_user FOREIGN KEY (assignated_id) REFERENCES user_land.user (id);
ALTER TABLE data.epic
    ADD CONSTRAINT fk_epic_description FOREIGN KEY (description_id) REFERENCES data.description (id);

-- Project
ALTER TABLE data.project
    ADD CONSTRAINT fk_project_reporter_user FOREIGN KEY (reported_id) REFERENCES user_land.user (id);
ALTER TABLE data.project
    ADD CONSTRAINT fk_project_assignated_user FOREIGN KEY (assignated_id) REFERENCES user_land.user (id);
ALTER TABLE data.project
    ADD CONSTRAINT fk_project_team FOREIGN KEY (assignated_team_id) REFERENCES user_land.team (id);
ALTER TABLE data.project
    ADD CONSTRAINT fk_project_description FOREIGN KEY (description_id) REFERENCES data.description (id);

-- Users_Roles
ALTER TABLE user_land.users_roles
    ADD CONSTRAINT fk_users_roles_user FOREIGN KEY (user_id) REFERENCES user_land.user (id);
ALTER TABLE user_land.users_roles
    ADD CONSTRAINT fk_users_roles_roles FOREIGN KEY (role_id) REFERENCES user_land.roles (id);

-- Users_Team
ALTER TABLE user_land.users_team
    ADD CONSTRAINT fk_users_team_user FOREIGN KEY (user_id) REFERENCES user_land.user (id);
ALTER TABLE user_land.users_team
    ADD CONSTRAINT fk_users_team_team FOREIGN KEY (team_id) REFERENCES user_land.team (id);

-- Team
-- Users_Roles
ALTER TABLE user_land.team
    ADD CONSTRAINT fk_team_roles FOREIGN KEY (default_role) REFERENCES user_land.roles (id);

-- COMMIT;
ROLLBACK;