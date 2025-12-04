------------------------------------------------------------
-- 1) 기존 객체 제거 (DROP)
------------------------------------------------------------

-- SEQUENCES
DROP SEQUENCE SEQ_TM;
DROP SEQUENCE SEQ_CATS;
DROP SEQUENCE SEQ_SUB_CATS;
DROP SEQUENCE SEQ_FIXED_EXP;
DROP SEQUENCE SEQ_TX;
DROP SEQUENCE SEQ_BUDGETS;

-- TABLES (FK 순서 고려하여 역순 DROP)
DROP TABLE INSTALLMENTS CASCADE CONSTRAINTS;
DROP TABLE TRANSACTIONS CASCADE CONSTRAINTS;
DROP TABLE FIXED_EXPENSES CASCADE CONSTRAINTS;
DROP TABLE BUDGETS CASCADE CONSTRAINTS;
DROP TABLE SUB_CATEGORIES CASCADE CONSTRAINTS;
DROP TABLE CATEGORIES CASCADE CONSTRAINTS;
DROP TABLE TRANSACTION_METHODS CASCADE CONSTRAINTS;

------------------------------------------------------------
-- 2) 시퀀스 생성
------------------------------------------------------------

CREATE SEQUENCE SEQ_TM          START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_CATS        START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_SUB_CATS    START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_FIXED_EXP   START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_TX          START WITH 1 INCREMENT BY 1;
CREATE SEQUENCE SEQ_BUDGETS     START WITH 1 INCREMENT BY 1;

------------------------------------------------------------
-- 3) 테이블 생성
------------------------------------------------------------

------------------------------------------------------------
-- 거래수단 (PAYMENT_METHODS → TRANSACTION_METHODS)
------------------------------------------------------------
CREATE TABLE TRANSACTION_METHODS (
    TM_ID        NUMBER         NOT NULL,
    NAME         VARCHAR2(50)   NOT NULL,
    TYPE         VARCHAR2(10)   NOT NULL,  -- BANK / CARD / CASH
    BALANCE      NUMBER         DEFAULT 0,
    BILLING_DAY  NUMBER         DEFAULT NULL,
    CONSTRAINT PK_TM PRIMARY KEY (TM_ID)
);

------------------------------------------------------------
-- 카테고리 (대분류)
------------------------------------------------------------
CREATE TABLE CATEGORIES (
    CATEGORY_ID  NUMBER          NOT NULL,
    NAME         VARCHAR2(50)    NOT NULL,
    TYPE         VARCHAR2(10)    NOT NULL, -- EXPENSE / INCOME
    CONSTRAINT PK_CATS PRIMARY KEY (CATEGORY_ID)
);

------------------------------------------------------------
-- 소분류 카테고리
------------------------------------------------------------
CREATE TABLE SUB_CATEGORIES (
    SUB_ID        NUMBER          NOT NULL,
    CATEGORY_ID   NUMBER          NOT NULL,
    NAME          VARCHAR2(50)    NOT NULL,
    CONSTRAINT PK_SUB_CATS PRIMARY KEY (SUB_ID),
    CONSTRAINT FK_SUB_CATS FOREIGN KEY (CATEGORY_ID)
        REFERENCES CATEGORIES(CATEGORY_ID)
);

------------------------------------------------------------
-- 예산 테이블 (카테고리별 예산)
------------------------------------------------------------
CREATE TABLE BUDGETS (
    BUDGET_ID    NUMBER         NOT NULL,
    CATEGORY_ID  NUMBER         NOT NULL,
    YYYYMM       VARCHAR2(6)    NOT NULL, -- '202511'
    AMOUNT       NUMBER         DEFAULT 0,
    CONSTRAINT PK_BUDGETS PRIMARY KEY (BUDGET_ID),
    CONSTRAINT FK_BUDGET_CATS FOREIGN KEY (CATEGORY_ID)
        REFERENCES CATEGORIES(CATEGORY_ID)
);

------------------------------------------------------------
-- 고정 지출 테이블
------------------------------------------------------------
CREATE TABLE FIXED_EXPENSES (
    FIXED_EXP_ID NUMBER          NOT NULL,
    SUB_ID       NUMBER          NOT NULL,
    TM_ID        NUMBER          NOT NULL,
    NAME         VARCHAR2(100)   NOT NULL,
    AMOUNT       NUMBER          NOT NULL,
    CYCLE_TYPE   VARCHAR2(10)    DEFAULT 'MONTHLY',
    CYCLE_DAY    NUMBER          NOT NULL,
    START_DATE   DATE            DEFAULT SYSDATE,
    END_DATE     DATE            DEFAULT NULL,
    CONSTRAINT PK_FIXED_EXP PRIMARY KEY (FIXED_EXP_ID),
    CONSTRAINT FK_FIXED_SUB FOREIGN KEY (SUB_ID)
        REFERENCES SUB_CATEGORIES(SUB_ID),
    CONSTRAINT FK_FIXED_TM FOREIGN KEY (TM_ID)
        REFERENCES TRANSACTION_METHODS(TM_ID)
);

------------------------------------------------------------
-- 거래내역 테이블
------------------------------------------------------------
CREATE TABLE TRANSACTIONS (
    TX_ID         NUMBER         NOT NULL,
    TM_ID         NUMBER         NOT NULL,
    SUB_ID        NUMBER         NOT NULL,
    FIXED_EXP_ID  NUMBER         DEFAULT NULL,
    AMOUNT        NUMBER         NOT NULL,
    TX_DATE       DATE           DEFAULT SYSDATE NOT NULL,
    MEMO          VARCHAR2(200),
    CREATED_AT    DATE           DEFAULT SYSDATE,
    CONSTRAINT PK_TX PRIMARY KEY (TX_ID),
    CONSTRAINT FK_TX_TM FOREIGN KEY (TM_ID)
        REFERENCES TRANSACTION_METHODS(TM_ID),
    CONSTRAINT FK_TX_SUB FOREIGN KEY (SUB_ID)
        REFERENCES SUB_CATEGORIES(SUB_ID),
    CONSTRAINT FK_TX_FIXED FOREIGN KEY (FIXED_EXP_ID)
        REFERENCES FIXED_EXPENSES(FIXED_EXP_ID)
);

------------------------------------------------------------
-- 할부 정보 테이블 (선택)
------------------------------------------------------------
CREATE TABLE INSTALLMENTS (
    TX_ID          NUMBER        NOT NULL,
    TOTAL_MONTHS   NUMBER        NOT NULL,
    CURRENT_MONTH  NUMBER        NOT NULL,
    CONSTRAINT PK_INST PRIMARY KEY (TX_ID),
    CONSTRAINT FK_INST_TX FOREIGN KEY (TX_ID)
        REFERENCES TRANSACTIONS(TX_ID) ON DELETE CASCADE
);

------------------------------------------------------------
-- 4) 초기 데이터 INSERT
------------------------------------------------------------

------------------------------------------------------------
-- 거래수단
------------------------------------------------------------
INSERT INTO TRANSACTION_METHODS VALUES (1, '신한은행', 'BANK', 5000000, NULL);
INSERT INTO TRANSACTION_METHODS VALUES (2, '현대카드', 'CARD', -450000, 25);
INSERT INTO TRANSACTION_METHODS VALUES (3, '현금', 'CASH', 30000, NULL);

------------------------------------------------------------
-- 카테고리
------------------------------------------------------------
INSERT INTO CATEGORIES VALUES (1, '식비', 'EXPENSE');
INSERT INTO CATEGORIES VALUES (2, '교통', 'EXPENSE');
INSERT INTO CATEGORIES VALUES (3, '쇼핑', 'EXPENSE');
INSERT INTO CATEGORIES VALUES (4, '급여', 'INCOME');

------------------------------------------------------------
-- 소분류
------------------------------------------------------------
INSERT INTO SUB_CATEGORIES VALUES (1, 1, '외식');
INSERT INTO SUB_CATEGORIES VALUES (2, 1, '카페/베이커리');
INSERT INTO SUB_CATEGORIES VALUES (3, 1, '배달음식');

INSERT INTO SUB_CATEGORIES VALUES (4, 2, '버스');
INSERT INTO SUB_CATEGORIES VALUES (5, 2, '지하철');

INSERT INTO SUB_CATEGORIES VALUES (6, 3, '의류');
INSERT INTO SUB_CATEGORIES VALUES (7, 3, '잡화');

INSERT INTO SUB_CATEGORIES VALUES (8, 4, '정기급여');
INSERT INTO SUB_CATEGORIES VALUES (9, 4, '상여금');

------------------------------------------------------------
-- 예산 데이터
------------------------------------------------------------
INSERT INTO BUDGETS VALUES (SEQ_BUDGETS.NEXTVAL, 1, '202511', 500000);
INSERT INTO BUDGETS VALUES (SEQ_BUDGETS.NEXTVAL, 2, '202511', 150000);
INSERT INTO BUDGETS VALUES (SEQ_BUDGETS.NEXTVAL, 3, '202511', 1000000);

------------------------------------------------------------
-- 고정 지출 데이터
------------------------------------------------------------
INSERT INTO FIXED_EXPENSES VALUES (
    SEQ_FIXED_EXP.NEXTVAL, 
    7, 2, 
    '넷플릭스', 
    17000, 
    'MONTHLY', 
    1, 
    SYSDATE, 
    NULL
);

------------------------------------------------------------
-- 거래내역
------------------------------------------------------------
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL, 1, 8, NULL,
    3000000,
    TO_DATE('2025-11-10 09:00', 'YYYY-MM-DD HH24:MI'),
    '11월 급여'
);

INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL, 2, 1, NULL,
    9000,
    TO_DATE('2025-11-11 12:30', 'YYYY-MM-DD HH24:MI'),
    '점심'
);

INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL, 2, 4, NULL,
    15000,
    TO_DATE('2025-11-12 23:40', 'YYYY-MM-DD HH24:MI'),
    '버스 충전'
);

INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL, 3, 3, NULL,
    4500,
    TO_DATE('2025-11-13 13:00', 'YYYY-MM-DD HH24:MI'),
    '배달 라면'
);

INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL, 2, 6, NULL,
    1200000,
    TO_DATE('2025-11-15 15:00', 'YYYY-MM-DD HH24:MI'),
    '의류 구매'
);

-- 의류 구매 건 → 할부 정보
INSERT INTO INSTALLMENTS VALUES (SEQ_TX.CURRVAL, 3, 1);

-- 고정 지출 자동 반영 예시
INSERT INTO TRANSACTIONS VALUES (
    SEQ_TX.NEXTVAL, 2, 7, 1,
    17000,
    TO_DATE('2025-11-01 10:00', 'YYYY-MM-DD HH24:MI'),
    '넷플릭스 자동결제'
);

COMMIT;
