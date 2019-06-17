# iaea-test-assignment

This project implement small API with backend on asp.net core and forntend on angular 7.
This task took about 7 hours:
* 2.5 hours were to download and install all required infrastructure (install SQL server, download npm packages, build default bootstrap project).
* 2 hours were spent to refreshing my angular knowledge.
* Unit tests took about 0.75 hours
* All ohther time was spent working on project and solving some unexpected problems with EF core and RxJs behaviour.

I had to spent additional time to implement transaction context, to avoid possible data inconsistency between db-requests from service methods. This took about hour, I tried to make transaction creation more elegant.

I skipped logging and decided to use JS prompt & alert to save time on implementing notifications (nothing hard, just time consuming).

Also, I skipped error reporting to user, because again get not enough time.
To be honest, I'm can't say that this assignment is implemented completely perfectly, but time is ran out anyway. 
