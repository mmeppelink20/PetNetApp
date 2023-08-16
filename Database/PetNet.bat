ECHO off

sqlcmd -S localhost -E -i PetNet_db_am.sql
sqlcmd -S localhost -E -i FosterApplicationResponse.sql


rem Add your sample data scripts to the bottom of this list
rem Follow this example (but without rem):
rem sqlcmd -S localhost -E -i SampleData\ASampleFeature_SD.sql
sqlcmd -S localhost -E -i SampleDataScripts\Zipcodes_DB.sql
sqlcmd -S localhost -E -i SampleDataScripts\PetNet_db_sample_data.sql
sqlcmd -S localhost -E -i SampleDataScripts\InstitutionalEntity_SD.sql
sqlcmd -S localhost -E -i SampleDataScripts\ViewUpdateFromFosterProfile_SD.sql
sqlcmd -S localhost -E -i SampleDataScripts\CustomerRecords_SD.sql
sqlcmd -S localhost -E -i SampleDataScripts\EditHoursOfOperation_SD.sql
sqlcmd -S localhost -E -i SampleDataScripts\FosterApplicationResponse.sql

sqlcmd -S localhost -E -i SampleDataScripts\ViewFundraisingEventsMVC_SD.sql

rem Add your stored procedure scripts to the bottom of this list
rem Follow this example (but without rem):
rem sqlcmd -S localhost -E -i StoredProcedures\MySampleFeature_SP.sql
rem sqlcmd -S localhost -E -i StoredProcedures\ASampleFeature_SP.sql

sqlcmd -S localhost -E -i StoredProceduresScripts\AddProcedure_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddAnimalProfile_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EmployeeManagement_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AnimalPostUpdate_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\MedicalProfile_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddAnimal_to_kennel_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewMedicalTests_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewMedicalAnimals_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewAllAdoptableAnimals_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewTreatment_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewKennel_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewAnimalProfile_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\RemoveKennels_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddKennel_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewAllShelterAnimals_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\MedicalImages.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InsertMedicalImage_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AnimalDOD513-514_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\sp_select_user_by_roleId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\sp_select_schedule_by_date_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\sp_select_schedule_by_userId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddSchedule_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditSchedule_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\DeleteSchedule_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\sp_user_creation.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddRole_to_User_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\RoleManagement_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewTicketList_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditProcedure_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\KenOccupancyUpdate-333_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\RemoveRole-006_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditTreatment_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditAnimalProfile_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddVaccination_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddMedicalRecord_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewVaccinationByAnimalId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewUsersByUsersId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditVaccination_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewFundraisingCampaigns_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewChangesToInventoryData_SP.sql
sqlcmd -S localhost -E -i "StoredProceduresScripts\FundraisingCampaign(Add,Edit,Delete)_SP.sql"
sqlcmd -S localhost -E -i StoredProceduresScripts\AddEditInstitutionalEntities_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddCampaignUpdate_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ReactivateAccount_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\DeactiveateShelterByShelterID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditShelterActiveByShelterID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditAreasOfNeedByShelterID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditEmailByShelterID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditPhoneByShelterID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditZipcodeByShelterID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditAddressTwoByShelterID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditAddressByShelterID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditShelterNameByShelterID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddShelter_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\SelectShelterByID_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\SelectShelterAll_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewAdoptableAnimalProfile_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AccountSettings_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\LogInUser_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\SuspendUserAccount_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewUpdatesFromFosterProfile_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ImageManagement_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewDonations_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddTicket.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InsertTestByMedicalRecordId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InsertTestMedicalRecordByAnimalId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\UpdateQuarantineStatusByMedicalRecordId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InsertItem_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InsertItemCategory_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\DeleteItemCategory_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\SelectAllCategories_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InsertCategory_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InsertItemTransaction_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewSpecificDonation_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewInstitutionalEntity_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewFundraisingEvents_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewFundraisingCampaign_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewRequest_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewMedicalRecordList.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewMedicalRecordDetailsByMedicalRecordId.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditExamNotes_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewExamNotes_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddExamNotes_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\CustomerRecords_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewShelterInventoryItemByShelterId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewShelterInventoryItemByShelterIdAndItemId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewInventoryItemCategoryByItemId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditShelterInventoryItem_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewDonor_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewEditResourceAddRequests_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ToggleFoster_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\MVCSetup_sp.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewEventByName.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewVolunteerByFundraisingEventId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewCommunityPage_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddPost_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditPost_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\Library_Stored_Procedures.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\FosterApplicationResponse_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AdoptionApplicationSPs.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\CloseTicket_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\UpdateFundraisingEventResults_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\RetrievePledgersByEventId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddReply_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditReply_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditUserShelter_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\Viewallevents.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InNetworkRequestCreate_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditHoursOfOperation_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\UpdatePostVisibility_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\DeleteReply_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ReportPost_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ReportReply_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddFundRaisingEvent_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditFundRaisingEvent_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\approve_deny_application_sps.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InsertResourceAddRequest_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewAllPledges_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\UpdateRequestAcknowledged.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\RemoveVolunteerFromEvent_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\SelectAllVolunteers_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\InsertVolunteerToEventbyVolunteerAndEventId_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewFundraisingEventsMVC_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\Bookmark_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\Surrender_SPs.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\sp_view_foster_applications.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EnterDonation_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\EditEvents_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\Viewalleventsbyvisabilty.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\FavoriteAndUnfavoritePost_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\HideReply_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\SortTicket_SP.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\ViewAllPrescriptions.sql
sqlcmd -S localhost -E -i StoredProceduresScripts\AddPrescriptions.sql

ECHO .
ECHO if no errors appear DB was created
PAUSE