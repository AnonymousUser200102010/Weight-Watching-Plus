Weight Watching (Program)+
==========
A program designed to track calories gained or lost over a day. Hopefully with more features forthcoming.

You also should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.

#General
This program is designed to not only make it convenient to track your weight, but easy and modular too. You are not restricted to books or online programs which charge monthly fees, but get a free, no nonsense, program that does everything you need it to! Well, not right now because I am still working on it but....

As you eat, you can enter items of food into the database and use them again, edit, or delete them later! You'll always have accurate calorie counts!

Don't want to track calories? Simply substitute everywhere it says calories with carbs (or your measurement of choice) and compile! I hope to have side-by-side functionality for carbs and calories in the future.

#Current OS Support
Currently the program is designed specifically for Windows, most specifically Windows 7. As I get the program more advanced and worked on, I'll attempt to make ports for Linux and OSX. I do not make guarantees on that though.

#Current Language Support
English is currently the only officially supported language, however the program is designed to be as language neutral as possible. You're more than welcome to push updates to development which include full translations.

#Other Design Specifications
1. Designed for .NET framework 4.5.1 (or lower)! The only other option for me (at the time of writing) is 4.5.2, which causes architecture problems for anything but Intel compatible processors. If you have one, you're welcome to upgrade it. For the sake of modulation, I will not.
2. *TBD*

#Features
Features include (but are not limited to):

Graphic User Interface

Ability to add, subtract, and manually edit from the calorie database in-program.

Registry Support -- certain attributes will remain constant as long as you're on the same computer.

Text File Support -- The food table remains a file for ease and transparency.

Database Search Support -- You do not have to manually look for a food item if you don't want to.

In-Program Database Editing, Deletion, and Addition -- You can add, remove, and change the values of food items without going into the text file.

Eating Before Bed Penalty -- If you eat anything within 8 hours of the reset time, there is a penalty.

Food Tracking Subset: Diary -- The part of food tracking which tracks what foods you've eaten over a day; this feature will continually write to the registry* and/or a file what you've eaten as you eat it. It does not track manual edits, so this data will not be available in the final food tracking product (as it stands).

Manual Reset Time -- this feature allows you to set the time the calories will reset, and it will remain at this value until you change it or disable the feature (not recommended for beginners!)

Under-eating/overeating benefit/subtraction -- this feature ensures people who eat less can have a ... few slip-ups, and those who have persistent overeating habits are penalized when the calories reset.

*Not Yet Implemented.

#Planned Features
Planned features are:

1. Sound support so it's not creepy while you use it.

2. Planning support so you can plan meals in advance.

3. Database drag&drop so you can have backups outside the program folder.

4. Food tracking -- A feature that requires more detailed explanation (see Adend1 at the bottom)

5. Exercise Support so that the program can accurately tailor calorie amounts to your specific needs.

7. Help Support so that you know what you're doing more clearly.

8. Usage Statistics Support -- various stats that tell you things like how often you use the program, how many hours used, etc; just fun factoids.

9. Protein, Fiber, Etc Support so the system is more accurate.

10. Database Tree Support so you can separate certain items from others and make the database easier to search.

11. Installer Support -- an installer that will install the program with the defined settings so you don't have to mess around with the executable or any of the libraries.

12. Hopefully more to come!

#Additional Notes
Adend1: *[Tracking what food you've eaten over a day, tracking what food items you eat the most and tracking how many calories you eat on average. All of which does not need to connect to the internet (no invasion of privacy)]*

#Use
1. Open in a program with Windows Forms support (I recommend SharpDevelop) and compile. A pre-compiled EXE is included for convenience.

2. Copy the compiled program (and any libraries that compile with it) to a new folder. Create a shortcut to the program (with any desired arguments) and run!

3. Make sure this program AND any libraries are INSIDE their own folder unless you know what you're doing!

_Usage within the program is fairly self-explanatory._

#Disclaimer
**I am not liable for any damage done to you, your property, or otherwise with regard to the running, compiling, and/or overall use of this program, it's associated libraries (if any), and/or any additional data contained within the source. I am likewise not financially, morally or legally obligated to pay for the cost of the aforementioned property, any lost wages, any emotional distress, and/or the repairs thereof.**

**This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.**

**I do not extend additional warranties and will not fulfil any warranty given to you by any third party or otherwise. By downloading this source, compiling the source from either (github, a third party website, or otherwise), and/or using (a) pre-compiled version(s) of this program from a third party website or otherwise, you agree to not only the license contained within this project, regardless of whether your version contained said license, but the information within this disclaimer and agree to all said information from the point you downloaded forward, _and if using a previous version you agree to any new additions or removals of/to said information_.**

**This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.**
 
**All users and/or distributors of the project who either plan to use this source in a fork, within a separate project, and/or to pre-package and give to others (provided said use does not go against the license contained within), are _strongly_ encouraged to add this disclaimer to any and all licenses/disclaimers contained within the product you are using, producing, and/or distributing so that the end-user is aware of said disclaimer. _I, again, am not liable for any damages related to said use_. As is outlined in this disclaimer.**

#Need Help?
If you have any questions about this project, contact me at [(PROTECTED EMAIL)] (edbcd860@opayq.com "edbcd860@opayq.com"). I do not give out my address for paper mail.

Please address the e-mail as **"Help with the project: Weight Watching +"** _(or "plus", I don't care which.)_! I have, on average, _several hundred_ e-mails from various sources, all of which are batch downloaded for viewing. I am known to just casually delete anything that even remotely looks like spam, so...yeah. Also, please try to send from a **trusted e-mail provider**, as junk mail is not batch downloaded and I've yet to find a way around this, and because I don't log into the actual account very often at all, it's very likely e-mails that end up in spam will stay in spam.