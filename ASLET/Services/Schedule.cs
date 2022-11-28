using System;
using System.Collections.Generic;
using System.Linq;

namespace ASLET.Models;

public class Schedule : Chromosome<Schedule>
{
    // Initializes chromosomes with configuration block (setup of chromosome)
    public Schedule(ConfigurationService configurationService)
    {
        ConfigurationService = configurationService;
        Fitness = 0;

        // reserve space for time-space slots in chromosomes code
        Slots = new List<SubjectClassModel>[Constants.DAYS_NUM * Constants.DAY_HOURS * ConfigurationService.NumberOfRooms];
        for (int i = 0; i < Slots.Length; ++i)
            Slots[i] = new();
        Classes = new();

        // reserve space for flags of class requirements
        Criteria = new bool[ConfigurationService.NumberOfCourseClasses * Constants.CRITERIA_NUM];
    }

    // Copy constructor
    private Schedule Copy(Schedule c, bool setupOnly)
    {
        if (!setupOnly)
        {
            // copy code
            Slots = c.Slots.ToArray();
            Classes = new(c.Classes);

            // copy flags of class requirements
            Criteria = c.Criteria.ToArray();

            // copy fitness
            Fitness = c.Fitness;
            ConfigurationService = c.ConfigurationService;
            return this;
        }

        return new Schedule(c.ConfigurationService);
    }

    // Makes new chromosome with same setup but with randomly chosen code
    public Schedule MakeNewFromPrototype(List<float> positions = null)
    {
        // make new chromosome, copy chromosome setup
        var newChromosome = Copy(this, true);

        // place classes at random position
        var c = ConfigurationService.CourseClasses;
        int nr = ConfigurationService.NumberOfRooms;
        foreach (var courseClass in c)
        {
            // determine random position of class
            int dur = courseClass.Duration;

            int day = ConfigurationService.Rand(0, Constants.DAYS_NUM - 1);
            int room = ConfigurationService.Rand(0, nr - 1);
            int time = ConfigurationService.Rand(0, (Constants.DAY_HOURS - 1 - dur));
            var reservation = Reservation.GetReservation(nr, day, time, room);

            if (positions != null)
            {
                positions.Add(reservation.Day * 1.0f);
                positions.Add(reservation.Room * 1.0f);
                positions.Add(reservation.Time * 1.0f);
            }

            // fill time-space slots, for each hour of class
            for (int i = dur - 1; i >= 0; --i)
                newChromosome.Slots[reservation.GetHashCode() + i].Add(courseClass);

            // insert in class table of chromosome
            newChromosome.Classes[courseClass] = reservation.GetHashCode();
        }

        newChromosome.CalculateFitness();
        return newChromosome;
    }

    // Performes crossover operation using to chromosomes and returns pointer to offspring
    public Schedule Crossover(Schedule parent2, int numberOfCrossoverPoints, float crossoverProbability)
    {
        // check probability of crossover operation
        if (ConfigurationService.Rand() % 100 > crossoverProbability)
            // no crossover, just copy first parent
            return Copy(this, false);

        // new chromosome object, copy chromosome setup
        var n = Copy(this, true);

        // number of classes
        int size = Classes.Count;

        var cp = new bool[size];

        // determine crossover point (randomly)
        for (int i = numberOfCrossoverPoints; i > 0; --i)
        {
            for (;;)
            {
                int p = ConfigurationService.Rand() % size;
                if (!cp[p])
                {
                    cp[p] = true;
                    break;
                }
            }
        }

        // make new code by combining parent codes
        bool first = ConfigurationService.Rand() % 2 == 0;
        for (int i = 0; i < size; ++i)
        {
            if (first)
            {
                var courseClass = Classes.Keys.ElementAt(i);
                var reservation = Classes[courseClass];
                // insert class from first parent into new chromosome's class table
                n.Classes[courseClass] = reservation;
                // all time-space slots of class are copied
                for (int j = courseClass.Duration - 1; j >= 0; --j)
                    n.Slots[reservation.GetHashCode() + j].Add(courseClass);
            }
            else
            {
                var courseClass = parent2.Classes.Keys.ElementAt(i);
                var reservation = parent2.Classes[courseClass];
                // insert class from second parent into new chromosome's class table
                n.Classes[courseClass] = reservation;
                // all time-space slots of class are copied
                for (int j = courseClass.Duration - 1; j >= 0; --j)
                    n.Slots[reservation.GetHashCode() + j].Add(courseClass);
            }

            // crossover point
            if (cp[i])
                // change source chromosome
                first = !first;
        }

        n.CalculateFitness();

        // return smart pointer to offspring
        return n;
    }

    // Performes crossover operation using to chromosomes and returns pointer to offspring
    public Schedule Crossover(Schedule parent, Schedule r1, Schedule r2, Schedule r3, float etaCross,
        float crossoverProbability)
    {
        // number of classes
        int size = Classes.Count;
        int jrand = ConfigurationService.Rand(size);

        // new chromosome object, copy chromosome setup
        var n = Copy(this, true);

        int nr = ConfigurationService.NumberOfRooms;
        for (int i = 0; i < size; ++i)
        {
            // check probability of crossover operation
            if (ConfigurationService.Rand() % 100 > crossoverProbability || i == jrand)
            {
                var courseClass = Classes.Keys.ElementAt(i);
                var reservation1 = Reservation.GetReservation(r1.Classes[courseClass]);
                var reservation2 = Reservation.GetReservation(r2.Classes[courseClass]);
                var reservation3 = Reservation.GetReservation(r3.Classes[courseClass]);

                // determine random position of class				
                int dur = courseClass.Duration;
                int day = (int)(reservation3.Day + etaCross * (reservation1.Day - reservation2.Day));
                if (day < 0)
                    day = 0;
                else if (day >= Constants.DAYS_NUM)
                    day = Constants.DAYS_NUM - 1;

                int room = (int)(reservation3.Room + etaCross * (reservation1.Room - reservation2.Room));
                if (room < 0)
                    room = 0;
                else if (room >= nr)
                    room = nr - 1;

                int time = (int)(reservation3.Time + etaCross * (reservation1.Time - reservation2.Time));
                if (time < 0)
                    time = 0;
                else if (time >= (Constants.DAY_HOURS - dur))
                    time = Constants.DAY_HOURS - 1 - dur;

                var reservation = Reservation.GetReservation(nr, day, time, room);

                // fill time-space slots, for each hour of class
                for (int j = courseClass.Duration - 1; j >= 0; --j)
                    n.Slots[reservation.GetHashCode() + j].Add(courseClass);

                // insert in class table of chromosome
                n.Classes[courseClass] = reservation.GetHashCode();
            }
            else
            {
                var courseClass = parent.Classes.Keys.ElementAt(i);
                var reservation = parent.Classes[courseClass];
                // insert class from second parent into new chromosome's class table
                n.Classes[courseClass] = reservation;
                // all time-space slots of class are copied
                for (int j = courseClass.Duration - 1; j >= 0; --j)
                    n.Slots[reservation.GetHashCode() + j].Add(courseClass);
            }
        }

        n.CalculateFitness();

        // return smart pointer to offspring
        return n;
    }

    private void Repair(SubjectClassModel cc1, int reservation1_index, Reservation reservation2)
    {
        int dur = cc1.Duration;
        int nr = ConfigurationService.NumberOfRooms;

        for (int j = dur - 1; j >= 0; --j)
        {
            // remove class hour from current time-space slot
            var cl = Slots[reservation1_index + j];
            cl.RemoveAll(cc => cc == cc1);
        }

        if (reservation2 == null)
        {
            int day = ConfigurationService.Rand(0, Constants.DAYS_NUM - 1);
            int room = ConfigurationService.Rand(0, nr - 1);
            int time = ConfigurationService.Rand(0, (Constants.DAY_HOURS - 1 - dur));
            reservation2 = Reservation.GetReservation(nr, day, time, room);
        }

        for (int j = dur - 1; j >= 0; --j)
        {
            // move class hour to new time-space slot
            Slots[reservation2.GetHashCode() + j].Add(cc1);
        }

        // change entry of class table to point to new time-space slots
        Classes[cc1] = reservation2.GetHashCode();
    }

    // Performs mutation on chromosome
    public void Mutation(int mutationSize, float mutationProbability)
    {
        // check probability of mutation operation
        if (ConfigurationService.Rand() % 100 > mutationProbability)
            return;

        // number of classes
        int numberOfClasses = Classes.Count;

        // move selected number of classes at random position
        for (int i = mutationSize; i > 0; --i)
        {
            // select random chromosome for movement
            int mpos = ConfigurationService.Rand() % numberOfClasses;

            // current time-space slot used by class
            var cc1 = Classes.Keys.ElementAt(mpos);
            Repair(cc1, Classes[cc1], null);
        }

        CalculateFitness();
    }

    // Calculates fitness value of chromosome
    public void CalculateFitness()
    {
        // chromosome's score
        int score = 0;

        int numberOfRooms = ConfigurationService.NumberOfRooms;
        int daySize = Constants.DAY_HOURS * numberOfRooms;

        int ci = 0;
        // check criteriaes and calculate scores for each class in schedule
        foreach (var cc in Classes.Keys)
        {
            // coordinate of time-space slot
            var reservation = Reservation.GetReservation(Classes[cc]);
            int day = reservation.Day;
            int time = reservation.Time;
            int room = reservation.Room;

            int dur = cc.Duration;

            // check for room overlapping of classes
            var ro = Models.Criteria.IsRoomOverlapped(Slots, reservation, dur);

            // on room overlapping
            if (!ro)
                score++;
            else
                score = 0;

            Criteria[ci + 0] = !ro;

            var r = ConfigurationService.GetRoomById(room);
            // does current room have enough seats
            Criteria[ci + 1] = Models.Criteria.IsSeatEnough(r, cc);
            if (Criteria[ci + 1])
                score++;
            else
                score /= 2;

            // does current room have computers if they are required
            Criteria[ci + 2] = Models.Criteria.IsComputerEnough(r, cc);
            if (Criteria[ci + 2])
                score++;
            else
                score /= 2;

            var total_overlap =
                Models.Criteria.IsOverlappedProfStudentGrp(Slots, cc, numberOfRooms, day * daySize + time);

            // professors have no overlapping classes?
            if (!total_overlap[0])
                score++;
            else
                score = 0;
            Criteria[ci + 3] = !total_overlap[0];

            // student groups has no overlapping classes?
            if (!total_overlap[1])
                score++;
            else
                score = 0;
            Criteria[ci + 4] = !total_overlap[1];

            ci += Constants.CRITERIA_NUM;
        }

        // calculate fitness value based on score
        Fitness = (float)score / Criteria.Length;
    }

    // Returns fitness value of chromosome
    public float Fitness { get; private set; }

    public ConfigurationService ConfigurationService { get; private set; }

    // Returns reference to table of classes
    public SortedDictionary<SubjectClassModel, int> Classes { get; private set; }

    // Returns array of flags of class requirements satisfaction
    public bool[] Criteria { get; private set; }

    // Return reference to array of time-space slots
    public List<SubjectClassModel>[] Slots { get; private set; }

    public float Diversity { get; set; }

    public int Rank { get; set; }

    public int GetDifference(Schedule other)
    {
        int val = 0;
        for (int i = 0; i < Criteria.Length && i < other.Criteria.Length; ++i)
        {
            if (Criteria[i] ^ other.Criteria[i])
                ++val;
        }

        return val;
    }

    public void ExtractPositions(float[] positions)
    {
        int i = 0;
        foreach (var cc in Classes.Keys)
        {
            var reservation = Reservation.GetReservation(Classes[cc]);
            positions[i++] = reservation.Day;
            positions[i++] = reservation.Room;
            positions[i++] = reservation.Time;
        }
    }

    public void UpdatePositions(float[] positions)
    {
        int nr = ConfigurationService.NumberOfRooms;
        int i = 0;
        var classes = Classes.Keys.ToArray();
        foreach (var cc in classes)
        {
            int dur = cc.Duration;
            int day = Math.Abs((int)positions[i] % Constants.DAYS_NUM);
            int room = Math.Abs((int)positions[i + 1] % nr);
            int time = Math.Abs((int)positions[i + 2] % (Constants.DAY_HOURS - cc.Duration));

            var reservation2 = Reservation.GetReservation(nr, day, time, room);
            Repair(cc, Classes[cc], reservation2);

            positions[i++] = reservation2.Day;
            positions[i++] = reservation2.Room;
            positions[i++] = reservation2.Time;
        }

        CalculateFitness();
    }
}