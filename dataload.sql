CREATE TABLE totservicerequests (
    unique_key text,
    created_date timestamp,
    closed_date timestamp,
    agency text,
    agency_name text,
    complaint_type text,
    descriptor text,
    location_type text,
    incident_zip text,
    incident_address text,
    street_name text,
    cross_street_1 text,
    cross_street_2 text,
    intersection_steet_1 text,
    intersection_street_2 text,
    address_type text,
    city text,
    landmark text, facility_type text,
    status text,
    due_date timestamp,
    resolution_description text,
    resolution_action_updated_date timestamp,
    community_board text,
    bbl text,
    borough text,
    x_coordinate_state_plane numeric,
    y_coordinate_state_plane numeric,
    open_data_channel_type text,
    park_facility_name text,
    park_borough text,
    vehicle_type text,
    taxi_company_borough text,
    taxi_pick_up_location text,
    bridge_highway_name text,
    bridge_highway_direction text,
    road_ramp text,
    bridge_highway_segment text,
    latitude decimal(10,7),
    longitude decimal(2,7),
    location_city text,
    location point,
    location_address text,
    location_zip text,
    location_state text
);

\COPY totservicerequests FROM './311_Service_Requests_from_03112019.csv' WITH (FORMAT csv);