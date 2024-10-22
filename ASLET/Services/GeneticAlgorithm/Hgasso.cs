﻿using System;
using System.Collections.Generic;
using System.Linq;
using ASLET.Models;

namespace ASLET.Services.GeneticAlgorithm;

public class Hgasso<T> : NsgaII<T> where T : Chromosome<T>
{
    private float _climax = .75f;
    private float _sgBestScore;
    private bool[] _motility;
    private float[] _sBestScore;
    private float[] _sgBest = null;
    private float[][] _current_position = null;
    private float[][] _sBest = null;
    private float[][] _velocity = null;

    // Initializes Hybrid Genetic Algorithm and Sperm Swarm Optimization
    public Hgasso(T prototype, int numberOfCrossoverPoints = 2, int mutationSize = 2, float crossoverProbability = 80,
        float mutationProbability = 3) : base(prototype, numberOfCrossoverPoints, mutationSize, crossoverProbability,
        mutationProbability)
    {
    }

    static E[][] CreateArray<E>(int rows, int cols)
    {
        E[][] array = new E[rows][];
        for (int i = 0; i < array.GetLength(0); i++)
            array[i] = new E[cols];

        return array;
    }

    protected override void Initialize(List<T> population)
    {
        int size = 0;
        int numberOfChromosomes = _populationSize;
        for (int i = 0; i < _populationSize; ++i)
        {
            List<float> positions = new();

            // initialize new population with chromosomes randomly built using prototype
            population.Add(_prototype.MakeNewFromPrototype(positions));

            if (i < 1)
            {
                size = positions.Count;
                _current_position = CreateArray<float>(numberOfChromosomes, size);
                _velocity = CreateArray<float>(numberOfChromosomes, size);
                _sBest = CreateArray<float>(numberOfChromosomes, size);
                _sgBest = new float[numberOfChromosomes];
                _sBestScore = new float[numberOfChromosomes];
                _motility = new bool[numberOfChromosomes];
            }

            _sBestScore[i] = population[i].Fitness;
            for (int j = 0; j < size; ++j)
            {
                _current_position[i][j] = positions[j];
                _velocity[i][j] = (float)(ConfigurationService.Rand(-.6464f, .7157f) / 3.0);
            }
        }
    }

    private void UpdateVelocities(List<T> population)
    {
        for (int i = 0; i < population.Count; ++i)
        {
            if (!_motility[i])
                continue;

            int dim = _velocity[i].Length;
            for (int j = 0; j < dim; ++j)
            {
                _velocity[i][j] = (float)(ConfigurationService.Random() * Math.Log10(ConfigurationService.Rand(7.0f, 14.0f)) *
                                          _velocity[i][j]
                                          + Math.Log10(ConfigurationService.Rand(7.0f, 14.0f)) *
                                          Math.Log10(ConfigurationService.Rand(35.5f, 38.5f)) *
                                          (_sBest[i][j] - _current_position[i][j])
                                          + Math.Log10(ConfigurationService.Rand(7.0f, 14.0f)) *
                                          Math.Log10(ConfigurationService.Rand(35.5f, 38.5f)) *
                                          (_sgBest[j] - _current_position[i][j]));

                _current_position[i][j] += _velocity[i][j];
            }
        }
    }

    protected override List<T> Replacement(List<T> population)
    {
        var populationSize = population.Count;
        var decline = 1 - _climax;

        for (int i = 0; i < populationSize; ++i)
        {
            var fitness = population[i].Fitness;
            if (fitness < _sBestScore[i])
            {
                population[i].UpdatePositions(_current_position[i]);
                fitness = population[i].Fitness;
                _motility[i] = true;
            }

            if (fitness > _sBestScore[i])
            {
                _sBestScore[i] = fitness;
                population[i].ExtractPositions(_current_position[i]);
                _sBest[i] = _current_position[i].ToArray();
            }

            if (fitness > _sgBestScore)
            {
                _sgBestScore = fitness;
                population[i].ExtractPositions(_current_position[i]);
                _sgBest = _current_position[i].ToArray();
            }

            if (_repeatRatio > _sBestScore[i])
                _sBestScore[i] -= _repeatRatio * decline;
            if (_repeatRatio > _climax && _sgBestScore > _climax)
            {
                if (i > (populationSize * _sgBestScore))
                {
                    population[i].UpdatePositions(_current_position[i]);
                    _motility[i] = true;
                }
            }
        }

        UpdateVelocities(population);
        return base.Replacement(population);
    }

    public override string ToString()
    {
        return "Hybrid Genetic Algorithm and Sperm Swarm Optimization (HGASSO)";
    }
}