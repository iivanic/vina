--
-- PostgreSQL database dump
--

-- Dumped from database version 17.3
-- Dumped by pg_dump version 17.3

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: categories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.categories (
    id integer NOT NULL,
    name_translation_key text NOT NULL
);


ALTER TABLE public.categories OWNER TO postgres;

--
-- Name: categories_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.categories ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.categories_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);

--
-- Name: countries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.countries (
    id integer NOT NULL,
    name text NOT NULL,
    is_active boolean DEFAULT true NOT NULL,
    lang text DEFAULT 'en'::text NOT NULL
);


ALTER TABLE public.countries OWNER TO postgres;

--
-- Name: countries_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.countries ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.countries_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: customers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.customers (
    id integer NOT NULL,
    email text NOT NULL,
    is_admin boolean DEFAULT false NOT NULL
);


ALTER TABLE public.customers OWNER TO postgres;

--
-- Name: customers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.customers ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.customers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: order_items; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.order_items (
    id integer NOT NULL,
    order_id integer NOT NULL,
    product_id integer NOT NULL,
    unit_price double precision NOT NULL,
    quantity integer NOT NULL
);


ALTER TABLE public.order_items OWNER TO postgres;

--
-- Name: order_items_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.order_items ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.order_items_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: order_status; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.order_status (
    id integer NOT NULL,
    status_translation_key text NOT NULL,
    is_closed boolean DEFAULT false NOT NULL
);


ALTER TABLE public.order_status OWNER TO postgres;

--
-- Name: order_status_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.order_status ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.order_status_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: orders; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.orders (
    id integer NOT NULL,
    order_time timestamp with time zone NOT NULL,
    payment_recieved_time timestamp with time zone,
    package_sent_time timestamp with time zone,
    package_recieveded_by_customer_time timestamp with time zone,
    amount double precision NOT NULL,
    phone_for_delivery text,
    state text,
    city text,
    address1 text,
    address2 text,
    comment text,
    status_id integer DEFAULT 1 NOT NULL,
    country_id integer DEFAULT 1 NOT NULL
);


ALTER TABLE public.orders OWNER TO postgres;

--
-- Name: orders_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.orders ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.orders_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: products; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.products (
    id integer NOT NULL,
    name_k text NOT NULL,
    description_k text,
    full_k text,
    price double precision DEFAULT 0 NOT NULL,
    max_order integer DEFAULT 6 NOT NULL,
    avalaible boolean DEFAULT true NOT NULL,
    published boolean DEFAULT false NOT NULL,
    category_id integer DEFAULT 1 NOT NULL

);


ALTER TABLE public.products OWNER TO postgres;

--
-- Name: products_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.products ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.products_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: tokens; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tokens (
    id integer NOT NULL,
    email text NOT NULL,
    valid_from date NOT NULL,
    valid_until date NOT NULL,
    challenge text NOT NULL,
    token text NOT NULL,
    is_revoked boolean DEFAULT false NOT NULL
);


ALTER TABLE public.tokens OWNER TO postgres;

--
-- Name: tokens_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.tokens ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.tokens_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: translations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.translations (
    id integer NOT NULL,
    key text NOT NULL,
    content text NOT NULL,
    lang text NOT NULL
);


ALTER TABLE public.translations OWNER TO postgres;

--
-- Name: translations_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.translations ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.translations_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);

-- logs
CREATE TABLE public.logs (
    id integer NOT NULL,
    log_timestamp timestamp with time zone,
    log_level text,
    category_name text,
    message text,
    exception text,
    CONSTRAINT logs_pkey PRIMARY KEY (id)
);
ALTER TABLE public.logs OWNER TO postgres;
CREATE INDEX IF NOT EXISTS idx_logs_id
    ON public.logs USING btree
    (id ASC NULLS LAST)
    WITH (deduplicate_items=False)
    ;
ALTER TABLE public.logs ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.logs_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);

CREATE TABLE public.zoho_mail (
    id integer NOT NULL,
    authorization_code text ,
    authorization_location text ,
    authorization_accountsServer text ,
    authorization_code_timestamp timestamp with time zone,
    access_token text ,
    access_token_type text ,
    access_token_timestamp timestamp with time zone,
    access_token_expires_in integer,
    access_token_valid_until timestamp with time zone,
    refresh_token text,
    api_domain text,
    CONSTRAINT zoho_mail_pkey PRIMARY KEY (id)
);
ALTER TABLE public.zoho_mail OWNER TO postgres;
CREATE INDEX IF NOT EXISTS idx_zoho_mail_id
    ON public.zoho_mail USING btree
    (id ASC NULLS LAST)
    WITH (deduplicate_items=False)
    ;

--from zoho
/*
INSERT INTO public.zoho_mail(
    id,
	authorization_code,
	authorization_location,
	authorization_accountsserver,
	authorization_code_timestamp,
	access_token,
	access_token_type,
	access_token_timestamp,
	access_token_expires_in,
	access_token_valid_until,
	refresh_token,
    api_domain)
	VALUES (
        1,
		'1000.f10889543cef8c0b1bf5c810a8683f8f.78f4b1642d23ed780a04c5d59c873965',
		'eu',
		'https://accounts.zoho.eu',
		'2025-02-25 10:19:10.737559+00', 
		'1000.0e7d623e491fb8c405fb40551b93d094.09bfb6b905632407c73f5a35ae8f9b82', 
		'Bearer', 
		'2025-02-25 10:19:46.752514+00', 
		3600, 
		'2025-02-25 11:19:46.752514+00',
		'1000.3e16fd2eca6a6c3ba8f583f7c8e5fd71.da8e80276ac298fe9de71f578e55f86e'
        '');
*/
    
--
-- Data for Name: categories; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.categories OVERRIDING SYSTEM VALUE VALUES (1, 'pukli_kamen_cat');
INSERT INTO public.categories OVERRIDING SYSTEM VALUE VALUES (2, 'dvije_ruze_cat');


--
-- Data for Name: countries; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.countries OVERRIDING SYSTEM VALUE VALUES (1, 'Hrvastka/Croatia', true, 'hr');
INSERT INTO public.countries OVERRIDING SYSTEM VALUE VALUES (2, 'Österreich/Austria', true, 'de');
INSERT INTO public.countries OVERRIDING SYSTEM VALUE VALUES (3, 'Deutschland/Germany', true, 'de');


--
-- Data for Name: customers; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.customers OVERRIDING SYSTEM VALUE VALUES (1, 'info@vina-ivanic.hr', true);


--
-- Data for Name: order_items; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: order_status; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.order_status OVERRIDING SYSTEM VALUE VALUES (3, 'status_order_dispatched', false);
INSERT INTO public.order_status OVERRIDING SYSTEM VALUE VALUES (4, 'status_order_recieved_by_customer', true);
INSERT INTO public.order_status OVERRIDING SYSTEM VALUE VALUES (5, 'status_order_canceled', true);
INSERT INTO public.order_status OVERRIDING SYSTEM VALUE VALUES (1, 'status_order_recieved', false);
INSERT INTO public.order_status OVERRIDING SYSTEM VALUE VALUES (2, 'status_order_payment_recieved', false);


--
-- Data for Name: orders; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: products; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.products OVERRIDING SYSTEM VALUE VALUES (1, 'pk_21', 'pk_21_desc', 'pk_21_full', 17, 6, true, true, 1);
INSERT INTO public.products OVERRIDING SYSTEM VALUE VALUES (2, 'pk_22', 'pk_22_desc', 'pk_22_full', 17, 6, true, true, 1);
INSERT INTO public.products OVERRIDING SYSTEM VALUE VALUES (3, 'dr_21', 'dr_21_desc', 'dr_21_full', 17, 6, true, true, 2);
INSERT INTO public.products OVERRIDING SYSTEM VALUE VALUES (4, 'dr_15', 'dr_15_desc', 'dr_15_full', 17, 6, true, true, 2);


--
-- Data for Name: tokens; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: translations; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (1, 'pukli_kamen_cat', 'Pukli kamen', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (2, 'pukli_kamen_cat', 'Cracked stone', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (3, 'pukli_kamen_cat', 'Pukli kamen', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (4, 'dvije_ruze_cat', 'Dvije ruže', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (5, 'dvije_ruze_cat', 'Two roses', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (6, 'dvije_ruze_cat', 'Zwei Rosen', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (7, 'pk_21', 'Pukli kamen 2021', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (8, 'pk_21', 'Cracked stone 2021', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (9, 'pk_21', 'Pukli kamen 2021', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (10, 'pk_22', 'Pukli kamen 2022', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (11, 'pk_22', 'Cracked stone 2022', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (12, 'pk_22', 'Pukli kamen 2022', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (13, 'dr_21', 'Dvije ruže 2021', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (14, 'dr_21', 'Two roses', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (15, 'dr_21', 'Zwei Rosen', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (16, 'pk_21_desc', 'Amber wine made from our best grapes of our local variety "Skrlet", in which we treat its''s delicate white grapes as if it was red variety. Fermented on skins until dryness with pigéage every 8 hrs, aged in used barique.', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (17, 'pk_21_desc', 'Unser Bernstein Wein wird aus unseren besten Trauben unserer lokalen Sorte "Skrlet" hergestellt, in der wir ihre zarten weißen Trauben behandeln, als wäre es eine rote Sorte. Gärung auf Schalen bis zur Trockenheit mit Schweinefleisch alle 8 Stunden, Reifung in gebrauchten Bariques.', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (18, 'pk_21_desc', 'Jantarno vino od našeg najboljeg škrleta. Vino autohtone sorte škrlet napravljeno tehnologijom crnog vina. Fermentirano na kožicama do suhoće uz pigéage svakih 8 sati, odležavano u rabljenim barrique bačvicama.', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (19, 'pk_22_desc', 'Amber wine made from our best grapes of our local variety "Skrlet", in which we treat its''s delicate white grapes as if it was red variety. Fermented on skins until dryness with pigéage every 8 hrs, aged in used barique.', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (20, 'pk_22_desc', 'Unser Bernstein Wein wird aus unseren besten Trauben unserer lokalen Sorte "Skrlet" hergestellt, in der wir ihre zarten weißen Trauben behandeln, als wäre es eine rote Sorte. Gärung auf Schalen bis zur Trockenheit mit Schweinefleisch alle 8 Stunden, Reifung in gebrauchten Bariques.', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (21, 'pk_22_desc', 'Jantarno vino od našeg najboljeg škrleta. Vino autohtone sorte škrlet napravljeno tehnologijom crnog vina. Fermentirano na kožicama do suhoće uz pigéage svakih 8 sati, odležavano u rabljenim barrique bačvicama.', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (22, 'dr_21_desc', 'Our best pinot noir.', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (23, 'dr_21_desc', 'Unser bester Spätburgunder. Eine Hommage an das Burgund.', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (24, 'dr_21_desc', 'Naš najbolji pinot crni. Hommage Burgundiji.', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (25, 'dr_15_desc', 'Our best pinot noir.', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (26, 'dr_15_desc', 'Unser bester Spätburgunder. Eine Hommage an das Burgund.', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (27, 'dr_15_desc', 'Naš najbolji pinot crni. Hommage Burgundiji.', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (28, 'pk_21_full', 'Balance between varietal expression and extraction. 12% alc.', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (29, 'pk_21_full', 'Gleichgewicht zwischen sortenreiner Ausprägung und Extraktion. 12 % Alk.', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (30, 'pk_21_full', 'Balans sortnosti i ekstrakcije. 12% alkohola.', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (31, 'pk_22_full', 'Structured. and ripe. 13% alc.', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (32, 'pk_22_full', 'Strukturiert. und reif. 13 % Alk.', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (33, 'pk_22_full', 'Strukturno i zrelo, 13% alkohola', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (34, 'dr_21_full', 'Fresh, delicious. forest floor', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (35, 'dr_21_full', 'Frisch, lecker. Waldboden', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (36, 'dr_21_full', 'Svježe, slasno, šumski pod, ', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (37, 'dr_15_full', 'Spicy notes and chocolate, ready', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (38, 'dr_15_full', 'Würzige Noten und Schokolade, fertig zum Trinken', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (39, 'dr_15_full', 'Začinske note i čokoloda, spremno', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (40, 'mission', '<p>Wine is not only an agricultural product. Wine represents a philosophy of life, it is a product with terroir.</p><p<Ultimately, our wines express the character of our vineyards, a character more persistent than the mood of an individual vintage.</p> ', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (41, 'mission', '<p>Vino nije samo poljoprivredni proizvod. Vino predstavlja životnu filozofiju, ono je proizvod s terroirom.</p><p<U konačnici, naša vina izražavaju osobnost naših vinograda, osobnost postojaniju od raspoloženja pojedine berbe.</p> ', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (42, 'mission', 'Wein ist nicht nur ein landwirtschaftliches Produkt. Wein stellt eine Lebensphilosophie dar, er ist ein Produkt mit Terroir.</p><p<Letztlich drücken unsere Weine den Charakter unserer Weinberge aus, der nachhaltiger ist als die Stimmung eines einzelnen Jahrgangs.</p> ', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (43, 'title', 'Vinarija Ivanić iz Kutine', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (44, 'title', 'Weingut Ivanic aus Kutina', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (45, 'title', 'Ivanić winery from Kutina', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (46, 'status_order_recieved', 'Bestellung erhalten', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (47, 'status_order_recieved', 'Narudžba zaprimljena', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (48, 'status_order_recieved', 'Order recieved', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (49, 'status_order_payment_recieved', 'Zahlung erhalten', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (50, 'status_order_payment_recieved', 'Uplata zaprimljena', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (51, 'status_order_payment_recieved', 'Payment recieved', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (52, 'status_order_dispatched', 'Paket verschickt', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (53, 'status_order_dispatched', 'Pošiljka poslana', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (54, 'status_order_dispatched', 'Package dispathed', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (55, 'status_order_recieved_by_customer', 'Paket geliefert', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (56, 'status_order_recieved_by_customer', 'Pošiljka isporučena', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (57, 'status_order_recieved_by_customer', 'Package delivered', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (58, 'status_order_canceled', 'Bestellung storniert', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (59, 'status_order_canceled', 'Narudđba otkazana', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (60, 'status_order_canceled', 'Order canceled', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (61, 'dr_15', 'Dvije ruže 2015', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (62, 'dr_15', 'Two roses', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (63, 'dr_15', 'Zwei Rosen', 'de');

INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (64, 'token_mail_subject', 'Prijava na stranice Vinarije Ivanić', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (65, 'token.mail_body1', 'Prijava na stranice Vinarije Ivanić', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (66, 'token_mail_body2', 'klikom na donji link možete se prijaviti na našu stranicu.', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (67, 'token_mail_body3', 'Link važi slijedećih 12h.', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (68, 'token_mail_signature', 'Vinarija Ivanić', 'hr');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (79, 'token_mail_click_here', 'Klimnite OVDJE za prijavu', 'hr');


INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (69, 'token_mail_subject', 'Anmeldung auf der Website des Weinguts Ivanic', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (70, 'token.mail_body1', 'Sehr geehrte/r,', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (71, 'token_mail_body2', 'Sie können sich auf unserer Site anmelden, indem Sie auf den untenstehenden Link klicken.', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (72, 'token_mail_body3', 'Link gültig für die nächsten 12 Stunden.', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (73, 'token_mail_signature', 'Weingut Ivanic', 'de');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (80, 'token_mail_click_here', 'Klicken Sie HIER, um sich anzumelden', 'de');


INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (74, 'token_mail_subject', 'Login to the Ivanic Winery website', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (75, 'token.mail_body1', 'Dear Vistor,', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (76, 'token_mail_body2', 'by clicking on the link below you can log in to our website.', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (77, 'token_mail_body3', 'The link is valid for the next 12 hours.', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (78, 'token_mail_signature', 'Ivanic Winery', 'en');
INSERT INTO public.translations OVERRIDING SYSTEM VALUE VALUES (81, 'token_mail_click_here', 'Click HERE to login', 'en');




--
-- Name: categories_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.categories_id_seq', 2, true);


--
-- Name: countries_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.countries_id_seq', 3, true);


--
-- Name: customers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.customers_id_seq', 1, true);


--
-- Name: order_items_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.order_items_id_seq', 1, false);


--
-- Name: order_status_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.order_status_id_seq', 5, true);


--
-- Name: orders_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.orders_id_seq', 1, false);


--
-- Name: products_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.products_id_seq', 1, false);


--
-- Name: tokens_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tokens_id_seq', 1, false);


--
-- Name: translations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.translations_id_seq', 60, true);


--
-- Name: categories categories_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY (id);


--
-- Name: countries countries_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.countries
    ADD CONSTRAINT countries_pkey PRIMARY KEY (id);


--
-- Name: customers customers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (id);


--
-- Name: order_items order_items_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT order_items_pkey PRIMARY KEY (id);


--
-- Name: order_status order_status_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_status
    ADD CONSTRAINT order_status_pkey PRIMARY KEY (id);


--
-- Name: orders orders_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT orders_pkey PRIMARY KEY (id);


--
-- Name: products products_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT products_pkey PRIMARY KEY (id);


--
-- Name: tokens tokens_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tokens
    ADD CONSTRAINT tokens_pkey PRIMARY KEY (id);


--
-- Name: translations translations_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.translations
    ADD CONSTRAINT translations_pkey PRIMARY KEY (id);


--
-- Name: fki_fk_order_items_orders; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_fk_order_items_orders ON public.order_items USING btree (order_id);


--
-- Name: fki_fk_order_items_products; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_fk_order_items_products ON public.order_items USING btree (product_id);


--
-- Name: fki_fk_orders_countries; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_fk_orders_countries ON public.orders USING btree (country_id);


--
-- Name: fki_fk_orders_order_status; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX fki_fk_orders_order_status ON public.orders USING btree (status_id);


--
-- Name: is_order_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX is_order_id ON public.orders USING btree (id) WITH (deduplicate_items='true');


--
-- Name: ix_cat_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_cat_id ON public.categories USING btree (id) WITH (deduplicate_items='true');


--
-- Name: ix_countries_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_countries_id ON public.countries USING btree (id) WITH (deduplicate_items='true');


--
-- Name: ix_cust_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_cust_id ON public.customers USING btree (id) WITH (deduplicate_items='true');


--
-- Name: ix_email; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_email ON public.customers USING btree (email) WITH (deduplicate_items='true');


--
-- Name: ix_prod_avail; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_prod_avail ON public.products USING btree (avalaible) WITH (deduplicate_items='true');


--
-- Name: ix_prod_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_prod_id ON public.products USING btree (id) WITH (deduplicate_items='true');


--
-- Name: ix_prod_published; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_prod_published ON public.products USING btree (published) WITH (deduplicate_items='true');


--
-- Name: ix_token_email; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_token_email ON public.tokens USING btree (email) WITH (deduplicate_items='true');


--
-- Name: ix_token_token; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_token_token ON public.tokens USING btree (token) WITH (deduplicate_items='true');


--
-- Name: ix_token_valid; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_token_valid ON public.tokens USING btree (valid_until) WITH (deduplicate_items='true');


--
-- Name: ix_trans_key; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_trans_key ON public.translations USING btree (key) WITH (deduplicate_items='true');


--
-- Name: ix_trans_lang; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX ix_trans_lang ON public.translations USING btree (lang) WITH (deduplicate_items='true');


--
-- Name: order_items fk_order_items_orders; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT fk_order_items_orders FOREIGN KEY (order_id) REFERENCES public.orders(id) NOT VALID;


--
-- Name: order_items fk_order_items_products; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.order_items
    ADD CONSTRAINT fk_order_items_products FOREIGN KEY (product_id) REFERENCES public.products(id) NOT VALID;


--
-- Name: orders fk_orders_countries; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT fk_orders_countries FOREIGN KEY (country_id) REFERENCES public.countries(id) NOT VALID;


--
-- Name: orders fk_orders_order_status; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.orders
    ADD CONSTRAINT fk_orders_order_status FOREIGN KEY (status_id) REFERENCES public.order_status(id) NOT VALID;


--
-- Name: products fk_prod_cat; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.products
    ADD CONSTRAINT fk_prod_cat FOREIGN KEY (category_id) REFERENCES public.categories(id) NOT VALID;


--
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: pg_database_owner
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;
GRANT ALL ON SCHEMA public TO PUBLIC;


--
-- PostgreSQL database dump complete
--

