if exists (select 1
            from  sysindexes
           where  id    = object_id('APPOINTMENT_SLOT')
            and   name  = 'BOOKED_AS_FK'
            and   indid > 0
            and   indid < 255)
   drop index APPOINTMENT_SLOT.BOOKED_AS_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('APPOINTMENT_SLOT')
            and   name  = 'PROVIDES_FK'
            and   indid > 0
            and   indid < 255)
   drop index APPOINTMENT_SLOT.PROVIDES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('APPOINTMENT_SLOT')
            and   type = 'U')
   drop table APPOINTMENT_SLOT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLINIC')
            and   type = 'U')
   drop table CLINIC
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('CLINICAL_NOTE')
            and   name  = 'DOCUMENTED_BY2_FK'
            and   indid > 0
            and   indid < 255)
   drop index CLINICAL_NOTE.DOCUMENTED_BY2_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('CLINICAL_NOTE')
            and   type = 'U')
   drop table CLINICAL_NOTE
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MEDICAL_VISIT')
            and   name  = 'DOCUMENTED_BY_FK'
            and   indid > 0
            and   indid < 255)
   drop index MEDICAL_VISIT.DOCUMENTED_BY_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MEDICAL_VISIT')
            and   name  = 'BOOKED_AS2_FK'
            and   indid > 0
            and   indid < 255)
   drop index MEDICAL_VISIT.BOOKED_AS2_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('MEDICAL_VISIT')
            and   name  = 'HAS_FK'
            and   indid > 0
            and   indid < 255)
   drop index MEDICAL_VISIT.HAS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('MEDICAL_VISIT')
            and   type = 'U')
   drop table MEDICAL_VISIT
go

if exists (select 1
            from  sysobjects
           where  id = object_id('OWNER')
            and   type = 'U')
   drop table OWNER
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('PET')
            and   name  = 'OWNS_FK'
            and   indid > 0
            and   indid < 255)
   drop index PET.OWNS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('PET')
            and   type = 'U')
   drop table PET
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('REMINDER')
            and   name  = 'TRIGGERS_FK'
            and   indid > 0
            and   indid < 255)
   drop index REMINDER.TRIGGERS_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('REMINDER')
            and   name  = 'RECEIVES_FK'
            and   indid > 0
            and   indid < 255)
   drop index REMINDER.RECEIVES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('REMINDER')
            and   type = 'U')
   drop table REMINDER
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('VACCINATION')
            and   name  = 'SUPPLIES_FK'
            and   indid > 0
            and   indid < 255)
   drop index VACCINATION.SUPPLIES_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('VACCINATION')
            and   name  = 'INCLUDES_FK'
            and   indid > 0
            and   indid < 255)
   drop index VACCINATION.INCLUDES_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('VACCINATION')
            and   type = 'U')
   drop table VACCINATION
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('VACCINE_INVENTORY')
            and   name  = 'STOCKS_FK'
            and   indid > 0
            and   indid < 255)
   drop index VACCINE_INVENTORY.STOCKS_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('VACCINE_INVENTORY')
            and   type = 'U')
   drop table VACCINE_INVENTORY
go

if exists (select 1
            from  sysobjects
           where  id = object_id('VETERINARIAN')
            and   type = 'U')
   drop table VETERINARIAN
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('VET_CLINIC')
            and   name  = 'HOSTS_FK'
            and   indid > 0
            and   indid < 255)
   drop index VET_CLINIC.HOSTS_FK
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('VET_CLINIC')
            and   name  = 'WORKS_AT_FK'
            and   indid > 0
            and   indid < 255)
   drop index VET_CLINIC.WORKS_AT_FK
go

if exists (select 1
            from  sysobjects
           where  id = object_id('VET_CLINIC')
            and   type = 'U')
   drop table VET_CLINIC
go

/*==============================================================*/
/* Table: APPOINTMENT_SLOT                                      */
/*==============================================================*/
create table APPOINTMENT_SLOT (
   SLOTID               int                  not null,
   ATTRIBUTE_70         int                  not null,
   VISITID              int                  not null,
   SLOTDATETIME         datetime             not null,
   DURATIONMINUTES      int                  null,
   STATUS               char(20)             null,
   constraint PK_APPOINTMENT_SLOT primary key nonclustered (SLOTID)
)
go

/*==============================================================*/
/* Index: PROVIDES_FK                                           */
/*==============================================================*/
create index PROVIDES_FK on APPOINTMENT_SLOT (
ATTRIBUTE_70 ASC
)
go

/*==============================================================*/
/* Index: BOOKED_AS_FK                                          */
/*==============================================================*/
create index BOOKED_AS_FK on APPOINTMENT_SLOT (
VISITID ASC
)
go

/*==============================================================*/
/* Table: CLINIC                                                */
/*==============================================================*/
create table CLINIC (
   CLINICID             int                  not null,
   CLINICNAME           char(100)            not null,
   LOCATION             char(255)            null,
   HASEMERGENCYFACILITY bit                  null,
   CLINICPHONE          char(20)             null,
   constraint PK_CLINIC primary key nonclustered (CLINICID)
)
go

/*==============================================================*/
/* Table: CLINICAL_NOTE                                         */
/*==============================================================*/
create table CLINICAL_NOTE (
   NOTEID               int                  not null,
   VISITID              int                  not null,
   WEIGHTKG             numeric(5,2)         null,
   DIAGNOSIS            text                 null,
   TREATMENTPLAN        text                 null,
   GENERALOBSERVATIONS  text                 null,
   RECORDEDAT           datetime             null,
   constraint PK_CLINICAL_NOTE primary key nonclustered (NOTEID)
)
go

/*==============================================================*/
/* Index: DOCUMENTED_BY2_FK                                     */
/*==============================================================*/
create index DOCUMENTED_BY2_FK on CLINICAL_NOTE (
VISITID ASC
)
go

/*==============================================================*/
/* Table: MEDICAL_VISIT                                         */
/*==============================================================*/
create table MEDICAL_VISIT (
   VISITID              int                  not null,
   NOTEID               int                  not null,
   PETID                int                  not null,
   SLOTID               int                  not null,
   VISITDATE            datetime             not null,
   VISITSTATUS          char(20)             null,
   constraint PK_MEDICAL_VISIT primary key nonclustered (VISITID)
)
go

/*==============================================================*/
/* Index: HAS_FK                                                */
/*==============================================================*/
create index HAS_FK on MEDICAL_VISIT (
PETID ASC
)
go

/*==============================================================*/
/* Index: BOOKED_AS2_FK                                         */
/*==============================================================*/
create index BOOKED_AS2_FK on MEDICAL_VISIT (
SLOTID ASC
)
go

/*==============================================================*/
/* Index: DOCUMENTED_BY_FK                                      */
/*==============================================================*/
create index DOCUMENTED_BY_FK on MEDICAL_VISIT (
NOTEID ASC
)
go

/*==============================================================*/
/* Table: OWNER                                                 */
/*==============================================================*/
create table OWNER (
   OWNERID              int                  not null,
   OFRISTNAME           char(50)             not null,
   OLASTNAME            char(50)             not null,
   OPHONE               char(15)             null,
   OEMAIL               char(100)            null,
   BILLINGADDRESS       char(255)            null,
   EMERGENCYCONTACT     char(100)            null,
   constraint PK_OWNER primary key nonclustered (OWNERID)
)
go

/*==============================================================*/
/* Table: PET                                                   */
/*==============================================================*/
create table PET (
   PETID                int                  not null,
   OWNERID              int                  not null,
   PETNAME              char(100)            not null,
   SPECIES              char(50)             not null,
   BREED                char(100)            null,
   AGE                  int                  null,
   constraint PK_PET primary key nonclustered (PETID)
)
go

/*==============================================================*/
/* Index: OWNS_FK                                               */
/*==============================================================*/
create index OWNS_FK on PET (
OWNERID ASC
)
go

/*==============================================================*/
/* Table: REMINDER                                              */
/*==============================================================*/
create table REMINDER (
   REMINDERID           int                  not null,
   OWNERID              int                  not null,
   VACCINATIONID        int                  not null,
   SCHEDULEDDATE        datetime             not null,
   CHANNEL              char(20)             null,
   REMINDESTATUS        char(20)             null,
   SENTAT               datetime             null,
   constraint PK_REMINDER primary key nonclustered (REMINDERID)
)
go

/*==============================================================*/
/* Index: RECEIVES_FK                                           */
/*==============================================================*/
create index RECEIVES_FK on REMINDER (
OWNERID ASC
)
go

/*==============================================================*/
/* Index: TRIGGERS_FK                                           */
/*==============================================================*/
create index TRIGGERS_FK on REMINDER (
VACCINATIONID ASC
)
go

/*==============================================================*/
/* Table: VACCINATION                                           */
/*==============================================================*/
create table VACCINATION (
   VACCINATIONID        int                  not null,
   VISITID              int                  not null,
   INVENTORYID          int                  not null,
   VACCINETYPE          char(100)            not null,
   ADMINISTEREDDATE     datetime             not null,
   NEXTBOOSTERDUE       datetime             null,
   constraint PK_VACCINATION primary key nonclustered (VACCINATIONID)
)
go

/*==============================================================*/
/* Index: INCLUDES_FK                                           */
/*==============================================================*/
create index INCLUDES_FK on VACCINATION (
VISITID ASC
)
go

/*==============================================================*/
/* Index: SUPPLIES_FK                                           */
/*==============================================================*/
create index SUPPLIES_FK on VACCINATION (
INVENTORYID ASC
)
go

/*==============================================================*/
/* Table: VACCINE_INVENTORY                                     */
/*==============================================================*/
create table VACCINE_INVENTORY (
   INVENTORYID          int                  not null,
   CLINICID             int                  not null,
   VACCINEINVENTORYTYPE char(100)            not null,
   BATCHNUMBER          char(50)             not null,
   SUPPLIERNAME         char(100)            null,
   EXPIRYDATE           datetime             null,
   QUANTITYAVAILABLE    int                  null,
   REORDERTHRESHOLD     int                  null,
   constraint PK_VACCINE_INVENTORY primary key nonclustered (INVENTORYID)
)
go

/*==============================================================*/
/* Index: STOCKS_FK                                             */
/*==============================================================*/
create index STOCKS_FK on VACCINE_INVENTORY (
CLINICID ASC
)
go

/*==============================================================*/
/* Table: VETERINARIAN                                          */
/*==============================================================*/
create table VETERINARIAN (
   VETID                int                  not null,
   VETFIRSTNAME         char(50)             not null,
   VETLASTNAME          char(50)             not null,
   SPECIALTY            char(100)            null,
   LICENSENUMBER        char(50)             null,
   VETPHONE             char(20)             null,
   constraint PK_VETERINARIAN primary key nonclustered (VETID)
)
go

/*==============================================================*/
/* Table: VET_CLINIC                                            */
/*==============================================================*/
create table VET_CLINIC (
   ATTRIBUTE_70         int                  not null,
   CLINICID             int                  not null,
   VETID                int                  not null,
   ISPRIMARY            bit                  null,
   JOINDATE             datetime             null,
   constraint PK_VET_CLINIC primary key nonclustered (ATTRIBUTE_70)
)
go

/*==============================================================*/
/* Index: WORKS_AT_FK                                           */
/*==============================================================*/
create index WORKS_AT_FK on VET_CLINIC (
VETID ASC
)
go

/*==============================================================*/
/* Index: HOSTS_FK                                              */
/*==============================================================*/
create index HOSTS_FK on VET_CLINIC (
CLINICID ASC
)
go
